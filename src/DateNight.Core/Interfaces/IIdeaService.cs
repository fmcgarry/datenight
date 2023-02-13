using DateNight.Core.Entities.IdeaAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IIdeaService
    {
        Task AddIdeaAsync(Idea idea);
    }
}