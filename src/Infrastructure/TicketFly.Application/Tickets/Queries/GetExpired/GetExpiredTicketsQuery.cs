namespace TicketFly.Application.Tickets.Queries.GetExpired;
public record GetExpiredTicketsQuery : IRequest<Result<IEnumerable<TicketDto>>>;