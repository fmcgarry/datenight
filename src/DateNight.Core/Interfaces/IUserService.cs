using DateNight.Core.Entities.UserAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IUserService
    {
        Task AddUserPartnerAsync(string id, string partnerId);

        Task<string> CreateUserAsync(string name, string email, string passwordText);

        Task DeleteUserAsync(string id);

        Task<User> GetUserbyEmailAsync(string email);

        Task<User> GetUserByIdAsync(string id);

        Task<IEnumerable<string>> GetUserPartners(string id);

        Task<string> LoginUserAsync(string username, string password);

        Task RemoveUserPartner(string id, string partnerId);

        Task UpdateUserAsync(string id, string name, string email, string passwordText);
    }
}