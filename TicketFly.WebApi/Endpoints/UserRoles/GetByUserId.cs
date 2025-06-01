using TicketFly.Application.UserRoles.Queries.GetRolesByUserId;
using TicketFly.Domain.Entities;
using TicketFly.WebApi.Extensions;
using TicketFly.WebApi.Infrastructure;

namespace TicketFly.WebApi.Endpoints.UserRoles;

public class GetByUserId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("userroles/{UserId:guid}", GetRoleByUserId)
            .WithTags(EndpointTags.UserRoles);
    }

    [Authorize]
    public static async Task<IResult> GetRoleByUserId(Guid UserId, ISender sender, CancellationToken cancellationToken)
    {
        Result<IEnumerable<Role>> result = await sender.Send(new GetRolesByUserIdQuery(UserId), cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

