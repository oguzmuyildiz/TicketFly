using TicketFly.Domain.Enums;

namespace TicketFly.Application.Tickets.Commands.SetStatus;
public record SetTicketStatusCommand(Guid Id, TicketStatus TicketStatus) : IRequest<Result<bool>>;