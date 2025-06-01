using TicketFly.Application.Roles.Queries.Get;
using TicketFly.Domain.Entities;
using TicketFly.WebApi.Extensions;
using TicketFly.WebApi.Infrastructure;

namespace TicketFly.WebApi.Endpoints.Roles;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("roles", GetRoles)
            .WithTags(EndpointTags.Roles);
    }

    [Authorize]
    public static async Task<IResult> GetRoles(ISender sender)
    {
        Result<IEnumerable<Role>> result = await sender.Send(new GetRolesQuery());
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
