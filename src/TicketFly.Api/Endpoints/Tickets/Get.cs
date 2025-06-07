using TicketFly.Api.Endpoints;
using TicketFly.Application.Tickets.Queries.Get;
using TicketFly.Domain.Constants;

namespace Api.Endpoints.Tickets;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("tickets", GetTickets)
            .WithTags(EndpointTags.Tickets)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> GetTickets(ISender sender)
    {
        Result<IEnumerable<TicketDto>> result = await sender.Send(new GetTicketsQuery());
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
