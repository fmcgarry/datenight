using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Core.Services
{
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
    }
}
