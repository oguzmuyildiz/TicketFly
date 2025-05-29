using Microsoft.AspNetCore.Authorization;
using TicketFly.Application.Clients.Queries.Get;
using TicketFly.Domain.Dtos;

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
        IEnumerable<ClientDto> clientDtos = await sender.Send(new GetClientsQuery());
        return Results.Ok(clientDtos);
    }
}
