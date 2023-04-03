using DateNight.Core.Interfaces;

namespace DateNight.Core.Entities.UserAggregate
{
    public class User : BaseEntity<Guid>, IAggregateRoot
    {
        public string Name { get; set; } = string.Empty;
    }
}