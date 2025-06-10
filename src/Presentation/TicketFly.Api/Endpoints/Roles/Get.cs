using TicketFly.Api.Endpoints;
using TicketFly.Application.Roles.Queries.Get;
using TicketFly.Domain.Constants;

namespace Api.Endpoints.Roles;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("roles", GetRoles)
            .WithTags(EndpointTags.Roles)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetRoles(ISender sender)
    {
        Result<IEnumerable<RoleDto>> result = await sender.Send(new GetRolesQuery());
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
