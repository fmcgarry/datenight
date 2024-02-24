using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DateNight.Infrastructure.Auth;

public class TokenService : ITokenService
{
    private readonly IAppLogger<TokenService> _logger;
    private readonly TokenServiceOptions _options;

    public TokenService(IAppLogger<TokenService> logger, IOptions<TokenServiceOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    public string GenerateToken(User user)
    {
        _logger.LogInformation("Generating token for user '{0}'", user.Email);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, _options.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud, _options.Audience)
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(new JwtHeader(signingCredentials), new JwtPayload(claims));
        string jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}