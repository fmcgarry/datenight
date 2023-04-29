using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Exceptions;
using DateNight.Core.Interfaces;
using System.Security.Cryptography;

namespace DateNight.Core.Services;

public class UserService : IUserService
{
    private const int PartnerCodeSize = 8;

    private readonly IAppLogger<UserService> _logger;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public UserService(IAppLogger<UserService> logger, IUserRepository userRepository, ITokenService tokenService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task AddUserPartnerAsync(string id, string partnerId)
    {
        var user = await GetUserByIdAsync(id);
        var partners = await _userRepository.GetUsersByPartialIdAsync(partnerId);

        if (partners.Count() > 1)
        {
            throw new ArgumentException($"More than one user id begins with {partnerId}");
        }

        var partner = partners.First();

        if (!user.Partners.Contains(partner.Id))
        {
            user.Partners.Add(partner.Id);
            await _userRepository.UpdateAsync(user);
        }

        if (!partner.Partners.Contains(user.Id))
        {
            partner.Partners.Add(id);
            await _userRepository.UpdateAsync(partner);
        }
    }

    public async Task<string> CreateUserAsync(string name, string email, string passwordText)
    {
        var existingUser = await _userRepository.GetByEmail(email);

        if (existingUser is not null)
        {
            throw new UserCreationFailedException("A user with the email already exists");
        }

        var password = CreateSecurePassword(passwordText);

        var user = new User()
        {
            Email = email,
            Name = name,
            Password = password
        };

        await GenerateValidUserId(user);

        await _userRepository.AddAsync(user);

        return user.Id.ToString();
    }

    public async Task DeleteUserAsync(string id)
    {
        var user = id.Contains('@') ? await GetUserbyEmailAsync(id) : await GetUserByIdAsync(id);

        await _userRepository.DeleteAsync(user);
        _logger.LogInformation("Deleted user '{id}'", id);
    }

    public async Task<User> GetUserbyEmailAsync(string email)
    {
        _logger.LogInformation("Getting user with email '{email}'", email);

        var user = await _userRepository.GetByEmail(email) ?? throw new UserDoesNotExistException(email);

        return user;
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        _logger.LogInformation("Getting user '{id}'", id);

        var user = await _userRepository.GetByIdAsync(id) ?? throw new UserDoesNotExistException(id);

        return user;
    }

    public async Task<IEnumerable<string>> GetUserPartners(string id)
    {
        var user = await GetUserByIdAsync(id);

        return user.Partners;
    }

    public async Task<string> LoginUserAsync(string username, string password)
    {
        var user = await GetUserbyEmailAsync(username);

        bool isValidPassword = VerifyPassword(password, user.Password);

        if (!isValidPassword)
        {
            throw new InvalidPasswordException();
        }

        var token = _tokenService.GenerateToken(user);

        return token;
    }

    public async Task RemoveUserPartner(string id, string partnerId)
    {
        var user = await GetUserByIdAsync(id);
        var partner = await GetUserByIdAsync(partnerId);

        user.Partners.Remove(partnerId);
        await _userRepository.UpdateAsync(user);

        partner.Partners.Remove(user.Id);
        await _userRepository.UpdateAsync(partner);
    }

    public async Task UpdateUserAsync(string id, string name, string email, string passwordText)
    {
        var user = await GetUserByIdAsync(id) ?? throw new UserDoesNotExistException(id);

        if (!string.IsNullOrEmpty(email))
        {
            user.Email = email;
            _logger.LogDebug("Updating user '{id}' email", id);
        }

        if (!string.IsNullOrEmpty(name))
        {
            user.Name = name;
            _logger.LogDebug("Updating user '{id}' name", id);
        }

        if (!string.IsNullOrEmpty(passwordText))
        {
            user.Password = CreateSecurePassword(passwordText);
            _logger.LogDebug("Updating user '{id}' password", id);
        }

        await _userRepository.UpdateAsync(user);
        _logger.LogInformation("Updated user '{id}'", id);
    }

    private static Password CreateSecurePassword(string passwordText)
    {
        using var hmac = new HMACSHA512();

        var password = new Password
        {
            Salt = hmac.Key,
            Hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText))
        };

        return password;
    }

    private static bool VerifyPassword(string passwordText, Password password)
    {
        using var hmac = new HMACSHA512(password.Salt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

        bool isMatch = computedHash.SequenceEqual(password.Hash);

        return isMatch;
    }

    private async Task GenerateValidUserId(User user)
    {
        const int MaxLoopCount = 10;

        for (int i = 0; i < MaxLoopCount; i++)
        {
            var existingUsers = await _userRepository.GetUsersByPartialIdAsync(user.Id[..PartnerCodeSize]);

            if (!existingUsers.Any())
            {
                return;
            }

            user.GenerateNewId();
        }

        throw new UserCreationFailedException($"Failed to generate a valid User Id after {MaxLoopCount} attempts.");
    }
}