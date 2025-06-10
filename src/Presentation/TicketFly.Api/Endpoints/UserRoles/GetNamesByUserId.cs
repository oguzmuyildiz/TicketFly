using TicketFly.Application.UserRoles.Queries.GetRoleNamesByUserId;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.UserRoles;

public class GetNamesByUserId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("userrolenames/{UserId:guid}", GetRoleNames)
            .WithTags(EndpointTags.UserRoles)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetRoleNames(Guid UserId, ISender sender, CancellationToken cancellationToken)
    {
        Result<IEnumerable<string>> result = await sender.Send(new GetRoleNamesByUserIdQuery(UserId), cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

