using TicketFly.Application.Clients.Commands.Create;

namespace TicketFly.WebApi.Endpoints.Clients;

public record CreateClientRequest(string Name, string Email, string Domain);

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("clients", CreateClient)
            .WithTags(EndpointTags.Clients);
    }

    public static async Task<IResult> CreateClient(CreateClientRequest createClientRequest, ISender sender, CancellationToken cancellationToken)
    {
        CreateClientCommand command = new(createClientRequest.Name, createClientRequest.Email, createClientRequest.Domain);
        Guid createdId = await sender.Send(command, cancellationToken);
        return Results.Ok(createdId);
    }
}
