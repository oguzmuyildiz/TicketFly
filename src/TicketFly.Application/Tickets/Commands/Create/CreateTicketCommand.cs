using TicketFly.Domain.Enums;

namespace TicketFly.Application.Tickets.Commands.Create;
public record CreateTicketCommand(string Title,
    string Content,
    string? Sender,
    string? Domain,
    TicketStatus Status,
    Guid? AssignedToId,
    Guid? ClientId) : IRequest<Result<Guid>>;