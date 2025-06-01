using TicketFly.Application.Clients.Commands.Create;
using TicketFly.Domain.Constants;
using TicketFly.WebApi.Extensions;
using TicketFly.WebApi.Infrastructure;

namespace TicketFly.WebApi.Endpoints.Clients;

public record CreateClientRequest(string Name, string Email, string Domain);

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("clients", CreateClient)
            .WithTags(EndpointTags.Clients)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> CreateClient(CreateClientRequest createClientRequest, ISender sender, CancellationToken cancellationToken)
    {
        CreateClientCommand command = new(createClientRequest.Name, createClientRequest.Email, createClientRequest.Domain);
        
        Result<Guid> result = await sender.Send(command, cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
