namespace TicketFly.Application.Clients.Commands.Create;
public record CreateClientCommand(Guid OrganizationId, string Name, string Email, string Domain) : IRequest<Result<Guid>>;