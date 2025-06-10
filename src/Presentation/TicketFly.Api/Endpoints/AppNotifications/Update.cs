using TicketFly.Application.AppNotifications.Commands.Update;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.AppNotifications;

public record UpdateAppNotificationRequest(
    Guid Id, 
    Guid? UserId, 
    Guid? OrganizationId, 
    Guid? RoleId, 
    string Title, 
    string Content, 
    string Link);

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("appnotification", UpdateAppNotification)
            .WithTags(EndpointTags.AppNotifications)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> UpdateAppNotification(UpdateAppNotificationRequest request, 
        ISender sender, CancellationToken cancellationToken)
    {
        UpdateAppNotificationCommand command = new(request.Id, request.UserId, request.OrganizationId, request.RoleId,
            request.Title, request.Content, request.Link);
        Result<bool> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
