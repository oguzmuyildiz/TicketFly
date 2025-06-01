using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Domain.Entities;
using TicketFly.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TicketFly.Infrastructure.Authentication;

internal sealed class TokenProvider(IConfiguration configuration, IUserContext userContext) : ITokenProvider
{
    private readonly IUserContext _userContext = userContext;
    public TokenModel Create(User user)
    {
        string secretKey = configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

       var roles = string.Join(",", user.UserRoles.Select(x=>x.Role.Name).ToArray());

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, roles),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpireMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        string AccessToken = handler.CreateToken(tokenDescriptor);
        string RefreshToken = GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Token = RefreshToken,
            UserId = user.Id,
            Created = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(30),
            CreatedByIp = _userContext.IpAddress,
            IsActive = true
        
        };

        user.RefreshTokens.Add(refreshToken);

        return new TokenModel(AccessToken, RefreshToken);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
