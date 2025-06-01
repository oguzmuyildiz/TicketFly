using TicketFly.Application.Roles.Queries.GetById;
using TicketFly.Domain.Entities;
using TicketFly.WebApi.Extensions;
using TicketFly.WebApi.Infrastructure;

namespace TicketFly.WebApi.Endpoints.Roles;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("roles/{id:guid}", GetRole)
            .WithTags(EndpointTags.Roles);
    }

    [Authorize]
    public static async Task<IResult> GetRole(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        Result<Role> result = await sender.Send(new GetRoleByIdQuery(id), cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

