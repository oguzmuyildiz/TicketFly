using TicketFly.Application.Clients.Commands.Create;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Clients;

public record CreateClientRequest(Guid OrganizationId, string Name, string Email, string Domain);

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("clients", CreateClient)
            .WithTags(EndpointTags.Clients)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> CreateClient(CreateClientRequest request, ISender sender, 
        CancellationToken cancellationToken)
    {
        Result<Guid> result = await sender.Send(
            new CreateClientCommand(request.OrganizationId, request.Name, request.Email, request.Domain), 
                cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
