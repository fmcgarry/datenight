using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Infrastructure.Repositories;

internal class IdeaMemoryRepository : MemoryRepository<Idea>, IIdeaRepository
{
    public Task<IEnumerable<Idea>> GetAllUserIdeasAsync(string userId)
    {
        return Task.FromResult(_objects.Where(idea => idea.CreatedBy.Equals(Guid.Parse(userId))));
    }
}