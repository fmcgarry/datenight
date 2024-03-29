﻿using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Infrastructure.Repositories;

internal class UserMemoryRepository : MemoryRepository<User>, IUserRepository
{
    public Task<User?> GetByEmail(string email)
    {
        return Task.FromResult(_objects.FirstOrDefault(x => x.Email.Equals(email)));
    }

    public Task<IEnumerable<User>> GetUsersByPartialIdAsync(string partialId)
    {
        return Task.FromResult(_objects.Where(x => x.Id.StartsWith(partialId)));
    }
}