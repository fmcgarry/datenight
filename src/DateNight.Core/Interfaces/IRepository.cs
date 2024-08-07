﻿namespace DateNight.Core.Interfaces;

public interface IRepository<T> where T : class, IAggregateRoot
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}