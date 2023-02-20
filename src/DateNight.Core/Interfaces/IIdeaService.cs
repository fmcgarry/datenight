using DateNight.Core.Entities.IdeaAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IIdeaService
    {
        Task AddIdeaAsync(Idea idea);

        Task<IEnumerable<Idea>> GetAllIdeasAsync();
        Task<Idea?> GetIdeaByIdAsync(string id);
        Task UpdateIdeaAsync(Idea idea);
    }
}