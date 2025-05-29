using System.Security.Claims;
using TicketFly.Application.Common.Intefaces.Authentication;


namespace TicketFly.WebApi.Services;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
