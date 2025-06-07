using TicketFly.Application.Tickets.Commands.Update;
using TicketFly.Domain.Constants;
using TicketFly.Domain.Enums;

namespace TicketFly.Api.Endpoints.Tickets;

public record UpdateTicketRequest(
    Guid Id, 
    string Title, 
    string Content, 
    string Sender, 
    string Domain, 
    TicketStatus Status, 
    Guid AssignedToId,
    Guid ClientId);

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("tickets", UpdateTicket)
            .WithTags(EndpointTags.Tickets)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> UpdateTicket(UpdateTicketRequest request, ISender sender, 
        CancellationToken cancellationToken)
    {
        Result<bool> result = await sender.Send(new UpdateTicketCommand(request.Id, request.Title, 
            request.Content, request.Sender, request.Domain, request.Status, request.AssignedToId, request.ClientId), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
