using DateNight.Core.Entities.IdeaAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IIdeaService
    {
        Task AddIdeaAsync(Idea idea);

        Task DeleteIdeaAsync(string id);

        Task<IEnumerable<Idea>> GetAllIdeasAsync();

        Task<Idea> GetIdeaByIdAsync(string id);

        Task<Idea> GetRandomIdeaAsync();

        Task UpdateIdeaAsync(Idea idea);
    }
}