using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Exceptions;
using DateNight.Core.Interfaces;
using System.Security.Cryptography;

namespace DateNight.Core.Services;

public class UserService : IUserService
{
    private readonly IAppLogger<UserService> _logger;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public UserService(IAppLogger<UserService> logger, IUserRepository userRepository, ITokenService tokenService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<string> CreateUserAsync(string name, string email, string password)
    {
        CreatePasswordHash(password, out byte[] hash, out byte[] salt);

        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = email,
            Name = name,
            Password = new Password
            {
                Hash = hash,
                Salt = salt
            }
        };

        await _userRepository.AddAsync(user);

        return user.Id.ToString();
    }

    public async Task<User> GetUserbyEmailAsync(string email)
    {
        _logger.LogInformation("Getting user with email '{email}'", email);

        var user = await _userRepository.GetByEmail(email) ?? throw new UserDoesNotExistException();

        return user;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        _logger.LogInformation("Getting user '{id}'", id);

        var user = await _userRepository.GetByIdAsync(id) ?? throw new UserDoesNotExistException();

        return user;
    }

    public async Task<string> LoginUserAsync(string username, string password)
    {
        var user = await GetUserbyEmailAsync(username);

        bool isValidPassword = VerifyPasswordHash(password, user.Password.Hash, user.Password.Salt);

        if (!isValidPassword)
        {
            throw new InvalidPasswordException();
        }

        var token = _tokenService.GenerateToken(user, user.Roles);

        return token;
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        bool isMatch = computedHash.SequenceEqual(hash);

        return isMatch;
    }
}