using TicketFly.Application.UserRoles.Queries.GetRoleNamesByUserId;
using TicketFly.WebApi.Extensions;
using TicketFly.WebApi.Infrastructure;

namespace TicketFly.WebApi.Endpoints.UserRoles;

public class GetNamesByUserId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("userrolenames/{UserId:guid}", GetRoleNames)
            .WithTags(EndpointTags.UserRoles);
    }

    [Authorize]
    public static async Task<IResult> GetRoleNames(Guid UserId, ISender sender, CancellationToken cancellationToken)
    {
        Result<IEnumerable<string>> result = await sender.Send(new GetRoleNamesByUserIdQuery(UserId), cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

