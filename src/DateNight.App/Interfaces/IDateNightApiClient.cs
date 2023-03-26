using DateNight.App.Models;

namespace DateNight.App.Interfaces;

public interface IDateNightApiClient
{
    Task CreateIdeaAsync(IdeaModel idea);

    Task DeleteIdeaAsync(IdeaModel idea);

    Task<IdeaModel?> GetActiveIdeaAsync();

    Task<IEnumerable<IdeaModel>> GetAllIdeasAsync();

    Task<IdeaModel> GetIdeaAsync(string id);

    Task<IdeaModel> GetRandomIdeaAsync();

    Task<UserModel> GetUserAsync(string id);

    Task SetIdeaAsActiveAsync(IdeaModel idea);

    Task UpdateIdeaAsync(IdeaModel idea);
}