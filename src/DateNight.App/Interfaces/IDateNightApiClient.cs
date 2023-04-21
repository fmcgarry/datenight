using DateNight.App.Models;

namespace DateNight.App.Interfaces;

public interface IDateNightApiClient
{
    Task CreateIdeaAsync(IdeaModel idea);

    Task CreateUserAsync(string name, string email, string password);

    Task DeleteIdeaAsync(IdeaModel idea);

    Task<IdeaModel?> GetActiveIdeaAsync();

    Task<IEnumerable<IdeaModel>> GetAllIdeasAsync();

    Task<IdeaModel> GetIdeaAsync(string id);

    Task<IdeaModel?> GetRandomIdeaAsync();

    Task<UserModel> GetUserAsync(string id);

    Task<bool> LoginUserAsync(string email, string password);

    Task SetIdeaAsActiveAsync(IdeaModel idea);

    Task UpdateIdeaAsync(IdeaModel idea);
}