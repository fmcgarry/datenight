using DateNight.Core.Interfaces;

namespace DateNight.Core.Entities;

public class BaseEntity<T> : IEntity
{
    public T? Id { get; protected set; }
}