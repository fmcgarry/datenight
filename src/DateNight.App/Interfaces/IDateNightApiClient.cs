using DateNight.App.Models;

namespace DateNight.App.Interfaces;

internal interface IDateNightApiClient
{
    Task CreateIdeaAsync(IdeaModel idea);

    Task DeleteIdeaAsync(IdeaModel idea);

    Task<IEnumerable<IdeaModel>> GetAllIdeasAsync();

    Task<IdeaModel> GetIdeaAsync(string id);

    Task<IdeaModel> GetRandomIdeaAsync();

    Task UpdateIdeaAsync(IdeaModel idea);
}