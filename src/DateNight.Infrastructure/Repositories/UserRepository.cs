using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public Task AddAsync(Idea entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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
            var user = new User()
            {
                Name = "Unknown",
                PaswordHash = Array.Empty<byte>(),
                PaswordSalt = Array.Empty<byte>(),
            };

            return Task.FromResult<User?>(user);
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