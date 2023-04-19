using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task AddAsync(User entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken = default)
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

        public Task<User?> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync<U>(U id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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