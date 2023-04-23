using DateNight.App.Components.IdeaComponent;
using DateNight.App.Models;

namespace DateNight.App.Clients.DateNightApi;

public interface IDateNightApiClient
{
    Task AddPartner(string code);

    Task CreateIdeaAsync(IdeaModel idea);

    Task CreateUserAsync(string name, string email, string password);

    Task DeleteIdeaAsync(IdeaModel idea);

    Task<IdeaModel?> GetActiveIdeaAsync();

    Task<IEnumerable<IdeaModel>> GetAllIdeasAsync();

    Task<UserModel?> GetCurrentUserAsync();

    Task<IdeaModel?> GetIdeaAsync(string id);

    Task<IdeaModel?> GetRandomIdeaAsync();

    Task<UserModel?> GetUserAsync(string id);

    Task<bool> LoginUserAsync(string email, string password);

    Task RemovePartner(string id);

    Task SetIdeaAsActiveAsync(IdeaModel idea);

    Task UpdateIdeaAsync(IdeaModel idea);

    Task UpdateUserAsync(string name, string email);

    Task UpdateUserPasswordAsync(string password);
}