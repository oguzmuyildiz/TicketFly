using TicketFly.Application.Clients.Commands.Update;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Clients;
public record UpdateClientRequest(Guid Id, string Name, string Email, string Domain);
public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("clients", UpdateClient)
            .WithTags(EndpointTags.Clients)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> UpdateClient(UpdateClientRequest request, 
        ISender sender, CancellationToken cancellationToken)
    {
        Result<bool> result = await sender.Send(
            new UpdateClientCommand(request.Id, request.Name, request.Email, request.Domain), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
