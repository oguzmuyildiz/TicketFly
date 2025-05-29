namespace TicketFly.Application.Clients.Commands.Update;

public record UpdateClientCommand(Guid Id, string Name, string Email, string Domain) : IRequest<bool>;