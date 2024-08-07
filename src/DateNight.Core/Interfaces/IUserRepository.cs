﻿using DateNight.Core.Entities.UserAggregate;

namespace DateNight.Core.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetByEmail(string email);

    public Task<IEnumerable<User>> GetUsersByPartialIdAsync(string partialId);
}