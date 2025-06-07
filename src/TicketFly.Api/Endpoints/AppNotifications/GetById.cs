using TicketFly.Application.AppNotifications.Queries.GetById;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.AppNotifications;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("appnotification/{id:guid}", GetAppNotification)
            .WithTags(EndpointTags.AppNotifications)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetAppNotification(Guid id, ISender sender, 
        CancellationToken cancellationToken)
    {
        Result<AppNotificationDto> result = await sender.Send(new GetAppNotificationsByIdQuery(id), 
            cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

