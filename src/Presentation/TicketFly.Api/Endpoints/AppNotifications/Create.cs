using TicketFly.Application.AppNotifications.Commands.Create;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.AppNotifications;

public record CreateAppNotificationRequest(
    Guid? UserId, 
    Guid? OrganizationId, 
    Guid? RoleId, 
    string Title, 
    string Content, 
    string Link);

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("appnotification", CreateAppNotification)
            .WithTags(EndpointTags.AppNotifications)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> CreateAppNotification(CreateAppNotificationRequest request, 
        ISender sender, CancellationToken cancellationToken)
    {
        CreateAppNotificationCommand command = new(request.UserId, 
            request.OrganizationId, request.RoleId, request.Title, request.Content, request.Link);
        
        Result<Guid> result = await sender.Send(command, cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}