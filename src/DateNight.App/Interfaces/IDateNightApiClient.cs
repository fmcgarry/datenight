using DateNight.App.Models;

namespace DateNight.App.Interfaces;

internal interface IDateNightApiClient
{
    Task CreateIdeaAsync(IdeaModel idea);

    Task DeleteIdeaAsync(IdeaModel idea);

    Task<IdeaModel> GetActiveIdeaAsync();

    Task<IEnumerable<IdeaModel>> GetAllIdeasAsync();

    Task<IdeaModel> GetIdeaAsync(string id);

    Task<IdeaModel> GetRandomIdeaAsync();

    Task SetIdeaAsActive(IdeaModel idea);

    Task UpdateIdeaAsync(IdeaModel idea);
}