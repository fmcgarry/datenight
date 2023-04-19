using DateNight.Core.Entities.UserAggregate;

namespace DateNight.Core.Interfaces;

public interface ITokenService
{
    public string GenerateToken(User user, IEnumerable<string> roles);
}