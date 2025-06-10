namespace TicketFly.Application.Tickets.Queries.GetByUserId;
public record GetTicketByUserIdQuery(Guid UserId) : IRequest<Result<TicketDto>>;