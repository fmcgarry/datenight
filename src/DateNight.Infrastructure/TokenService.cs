using DateNight.Core.Entities.UserAggregate;
using DateNight.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DateNight.Infrastructure;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IAppLogger<TokenService> _logger;

    public TokenService(IAppLogger<TokenService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public string GenerateToken(User user, IEnumerable<string> roles)
    {
        _logger.LogInformation("Generating token for user '{0}'", user.Email);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var apiToken = _configuration.GetRequiredSection("ApiKey").Value!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiToken));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(new JwtHeader(signingCredentials), new JwtPayload(claims));

        string jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}