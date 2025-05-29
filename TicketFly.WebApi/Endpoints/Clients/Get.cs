using TicketFly.Application.Clients.Queries.Get;

namespace TicketFly.WebApi.Endpoints.Clients;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("clients", GetClients)
            .WithTags(EndpointTags.Clients);
    }

    public static async Task<IResult> GetClients(ISender sender)
    {
        var vm = await sender.Send(new GetClientsQuery());
        return Results.Ok(vm);
    }
}
