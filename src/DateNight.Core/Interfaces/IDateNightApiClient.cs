using DateNight.Core.Entities.IdeaAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IDateNightApiClient
    {
        Task CreateIdeaAsync(Idea idea);
    }
}