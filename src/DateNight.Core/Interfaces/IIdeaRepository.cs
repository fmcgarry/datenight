using DateNight.Core.Entities.IdeaAggregate;

namespace DateNight.Core.Interfaces;

public interface IIdeaRepository : IRepository<Idea>
{
    Task<IEnumerable<Idea>> GetAllUserIdeasAsync(string userId);
}
