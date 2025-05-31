using TicketFly.Application.Clients.Queries.Get;
using TicketFly.Domain.Dtos;
using TicketFly.WebApi.Extensions;
using TicketFly.WebApi.Infrastructure;

namespace TicketFly.WebApi.Endpoints.Clients;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("clients", GetClients)
            .WithTags(EndpointTags.Clients);
    }

    [Authorize]
    public static async Task<IResult> GetClients(ISender sender)
    {
        Result<IEnumerable<ClientDto>> result = await sender.Send(new GetClientsQuery());
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
