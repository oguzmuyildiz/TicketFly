using TicketFly.Application.Clients.Queries.GetById;
using TicketFly.Domain.Dtos;

namespace TicketFly.WebApi.Endpoints.Clients;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("clients/{id:guid}", GetClient)
            .WithTags(EndpointTags.Clients);
    }

    public static async Task<IResult> GetClient(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        ClientDto result = await sender.Send(new GetClientByIdQuery(id), cancellationToken);
        return Results.Ok(result);
    }
}

