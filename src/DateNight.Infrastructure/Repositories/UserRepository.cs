using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private static readonly List<User> _users = new();

        public Task AddAsync(User entity, CancellationToken cancellationToken = default)
        {
            _users.Add(entity);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync<U>(U id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.Name == id!.ToString()));
        }

        public Task UpdateAsync(User entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}