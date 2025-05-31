namespace TicketFly.Application.Roles.Commands.Create;

public record CreateRoleCommand(string Name) : IRequest<Result<Guid>>;