using TicketFly.Application.Clients.Queries.GetById;
using TicketFly.Domain.Dtos;
using TicketFly.WebApi.Extensions;
using TicketFly.WebApi.Infrastructure;

namespace TicketFly.WebApi.Endpoints.Clients;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("clients/{id:guid}", GetClient)
            .WithTags(EndpointTags.Clients);
    }

    [Authorize]
    public static async Task<IResult> GetClient(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        Result<ClientDto> result = await sender.Send(new GetClientByIdQuery(id), cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

