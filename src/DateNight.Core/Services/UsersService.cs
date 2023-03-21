using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Core.Services
{
    public class UsersService : IUsersService
    {
        public User? GetUser(Guid id)
        {
            var user = new User();

            return user;
        }
    }
}
