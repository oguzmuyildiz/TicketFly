namespace TicketFly.Application.Tickets.Queries.Get;
public record GetTicketsQuery : IRequest<Result<IEnumerable<TicketDto>>>;