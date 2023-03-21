using DateNight.Core.Interfaces;

namespace DateNight.Core.Entities.UserAggregate
{
    public class User : BaseEntity<Guid>, IAggregateRoot
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Name => $"{FirstName} {LastName}";
    }
}
