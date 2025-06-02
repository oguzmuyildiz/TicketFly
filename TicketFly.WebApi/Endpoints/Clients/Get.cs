using TicketFly.Application.Clients.Queries.Get;
using TicketFly.Domain.Constants;
namespace TicketFly.WebApi.Endpoints.Clients;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("clients", GetClients)
            .WithTags(EndpointTags.Clients)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetClients(ISender sender, ILogger<Get> logger)
    {
        logger.LogInformation("Handling request to get all clients");

        Result<IEnumerable<ClientDto>> result = await sender.Send(new GetClientsQuery());
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
