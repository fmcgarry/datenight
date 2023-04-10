using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Interfaces;
using System.Security.Cryptography;

namespace DateNight.Core.Services;

public class UserService : IUserService
{
    private readonly IAppLogger<UserService> _logger;
    private readonly IRepository<User> _userRepository;

    public UserService(IAppLogger<UserService> logger, IRepository<User> userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserAsync(Guid id)
    {
        _logger.LogInformation("Getting user '{id}'", id);
        var user = await _userRepository.GetByIdAsync(id);

        return user;
    }

    public async Task<string> CreateUserAsync(string name, string password)
    {
        CreatePasswordHash(password, out var hash, out var salt);

        var user = new User()
        {
            Id = Guid.NewGuid(),
            Name = name,
            PaswordHash = hash,
            PaswordSalt = salt
        };

        // save user in userRepository

        return user.Id.ToString();
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
