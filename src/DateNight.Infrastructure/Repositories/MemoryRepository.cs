using DateNight.Core.Entities;
using DateNight.Core.Interfaces;

namespace DateNight.Infrastructure.Repositories
{
    internal class MemoryRepository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private readonly List<T> _objects = new();

        public Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _objects.Add(entity);

            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _objects.AddRange(entities);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _objects.Remove(entity);

            return Task.CompletedTask;
        }

        public Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            foreach (var idea in entities)
            {
                _objects.Remove(idea);
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_objects.AsEnumerable());
        }

        public Task<T?> GetByIdAsync<U>(U id, CancellationToken cancellationToken = default)
        {
            var idea = _objects.FirstOrDefault(x => (x as BaseEntity<U>)!.Id!.Equals(id));

            return Task.FromResult(idea);
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            int index = _objects.IndexOf(entity);
            _objects[index] = entity;

            return Task.CompletedTask;
        }

        public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                int index = _objects.IndexOf(entity);
                _objects[index] = entity;
            }

            return Task.CompletedTask;
        }
    }
}
