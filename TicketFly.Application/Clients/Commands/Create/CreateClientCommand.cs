namespace TicketFly.Application.Clients.Commands.Create;

public record CreateClientCommand(string Name, string Email, string Domain) : IRequest<Guid>;