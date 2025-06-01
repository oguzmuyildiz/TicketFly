using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicketFly.Application.Common.Intefaces.Authentication;

namespace TicketFly.WebApi.Services;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.NameId);
    public string? IpAddress => _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
}
