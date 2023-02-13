using DateNight.App.Models;

namespace DateNight.App.Interfaces;

internal interface IDateNightApiClient
{
    Task CreateIdeaAsync(IdeaModel idea);
    Task<IEnumerable<IdeaModel>> GetAllIdeasAsync();
}