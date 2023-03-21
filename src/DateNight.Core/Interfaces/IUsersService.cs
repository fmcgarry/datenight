using DateNight.Core.Entities.UserAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IUsersService
    {
        User? GetUser(Guid id);
    }
}