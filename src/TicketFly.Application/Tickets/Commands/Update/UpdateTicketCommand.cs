using TicketFly.Domain.Enums;

namespace TicketFly.Application.Tickets.Commands.Update;
public record UpdateTicketCommand(Guid Id, string Title,
    string Content,
    string? Sender,
    string? Domain,
    TicketStatus Status,
    Guid? AssignedToId,
    Guid? ClientId) : IRequest<Result<bool>>;
