using TicketFly.Application.Tickets.Queries.GetById;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Tickets;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("tickets/{id:guid}", GetTicket)
            .WithTags(EndpointTags.Tickets)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetTicket(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        Result<TicketDto> result = await sender.Send(new GetTicketByIdQuery(id), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}

