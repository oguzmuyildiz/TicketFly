using TicketFly.Application.Clients.Commands.Update;

namespace TicketFly.WebApi.Endpoints.Clients;

public record UpdateClientRequest(Guid Id, string Name, string Email, string Domain);

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("clients", UpdateClient)
            .WithTags(EndpointTags.Clients);
    }

    public static async Task<IResult> UpdateClient(UpdateClientRequest request, ISender sender, CancellationToken cancellationToken)
    {
        UpdateClientCommand command = new(request.Id, request.Name, request.Email, request.Domain);
        bool result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}
