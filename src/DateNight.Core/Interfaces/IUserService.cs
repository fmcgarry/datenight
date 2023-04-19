﻿using DateNight.Core.Entities.UserAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(string name, string email, string password);

        Task<User?> GetUserAsync(Guid id);

        Task<string> LoginUserAsync(string username, string password);
    }
}