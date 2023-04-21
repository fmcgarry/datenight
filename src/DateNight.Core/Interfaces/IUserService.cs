using DateNight.Core.Entities.UserAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(string name, string email, string passwordText);

        Task<User> GetUserbyEmailAsync(string email);

        Task<User> GetUserByIdAsync(string id);

        Task<string> LoginUserAsync(string username, string password);

        Task DeleteUserAsync(string id);

        Task UpdateUserAsync(string id, string name, string email, string passwordText);
    }
}