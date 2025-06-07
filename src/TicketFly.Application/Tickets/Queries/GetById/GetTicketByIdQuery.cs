namespace TicketFly.Application.Tickets.Queries.GetById;
public record GetTicketByIdQuery(Guid Id) : IRequest<Result<TicketDto>>;