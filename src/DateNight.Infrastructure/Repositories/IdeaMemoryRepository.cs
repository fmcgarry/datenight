using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Infrastructure.Repositories
{
    public class IdeaMemoryRepository : IRepository<Idea>
    {
        private readonly List<Idea> _ideas = new();

        public Task AddAsync(Idea entity, CancellationToken cancellationToken = default)
        {
            _ideas.Add(entity);

            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<Idea> entities, CancellationToken cancellationToken = default)
        {
            _ideas.AddRange(entities);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Idea entity, CancellationToken cancellationToken = default)
        {
            _ideas.Remove(entity);

            return Task.CompletedTask;
        }

        public Task DeleteRangeAsync(IEnumerable<Idea> entities, CancellationToken cancellationToken = default)
        {
            foreach (var idea in entities)
            {
                _ideas.Remove(idea);
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Idea>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_ideas.AsEnumerable());
        }

        public Task<Idea?> GetByIdAsync<U>(U id, CancellationToken cancellationToken = default)
        {
            var idea = _ideas.FirstOrDefault(x => x.Id!.Equals(id));

            return Task.FromResult(idea);
        }

        public Task UpdateAsync(Idea entity, CancellationToken cancellationToken = default)
        {
            int index = _ideas.IndexOf(entity);
            _ideas[index] = entity;

            return Task.CompletedTask;
        }

        public Task UpdateRangeAsync(IEnumerable<Idea> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                int index = _ideas.IndexOf(entity);
                _ideas[index] = entity;
            }

            return Task.CompletedTask;
        }
    }
}