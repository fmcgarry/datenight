using DateNight.Core.Interfaces;

namespace DateNight.Core.Entities.UserAggregate
{
    public class User : BaseEntity<Guid>, IAggregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public byte[] PaswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PaswordSalt { get; set; } = Array.Empty<byte>();
    }
}