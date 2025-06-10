using TicketFly.Application.AppNotifications.Queries.Get;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.AppNotifications;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("appnotification", GetAppNotifications)
            .WithTags(EndpointTags.AppNotifications)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetAppNotifications(ISender sender, ILogger<Get> logger)
    {
        logger.LogInformation("Handling request to get all AppNotifications");

        Result<IEnumerable<AppNotificationDto>> result = await sender.Send(new GetAppNotificationsQuery());
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
