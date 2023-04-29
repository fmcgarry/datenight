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

    public string GenerateToken(User user)
    {
        _logger.LogInformation("Generating token for user '{0}'", user.Email);

        string issuer = _configuration["JwtSettings:Issuer"]!;
        string audience = _configuration["JwtSettings:Audience"]!;

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, issuer),
            new Claim(JwtRegisteredClaimNames.Aud, audience)
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var apiToken = _configuration["JwtSettings:Key"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiToken));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(new JwtHeader(signingCredentials), new JwtPayload(claims));

        string jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}