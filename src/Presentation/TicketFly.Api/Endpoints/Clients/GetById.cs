using TicketFly.Application.Clients.Queries.GetById;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Clients;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("clients/{Id:guid}", GetClient)
            .WithTags(EndpointTags.Clients)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetClient(Guid Id, ISender sender, 
        CancellationToken cancellationToken)
    {
        Result<ClientDto> result = await sender.Send(new GetClientByIdQuery(Id), 
            cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

