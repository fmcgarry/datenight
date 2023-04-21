using DateNight.Core.Interfaces;

namespace DateNight.Core.Entities.UserAggregate
{
    public class User : BaseEntity, IAggregateRoot
    {
        public User()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Password Password { get; set; } = new();
        public List<string> Roles { get; set; } = new();
    }
}