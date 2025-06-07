using TicketFly.Application.AppNotifications.Queries.GetByUserId;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.AppNotifications;

public class GetByUserId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("user/{id:guid}/appnotifications", GetAppNotifications)
            .WithTags(EndpointTags.AppNotifications)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetAppNotifications(Guid UserId, ISender sender, 
        CancellationToken cancellationToken)
    {
        Result<IEnumerable<AppNotificationDto>> result = await sender.Send(new GetAppNotificationsByUserIdQuery(UserId), 
            cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

