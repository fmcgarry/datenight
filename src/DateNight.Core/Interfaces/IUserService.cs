using DateNight.Core.Entities.UserAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserAsync(Guid id);
    }
}