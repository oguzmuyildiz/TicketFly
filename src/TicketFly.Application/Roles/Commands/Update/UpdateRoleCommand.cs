namespace TicketFly.Application.Roles.Commands.Update;
public record UpdateRoleCommand(Guid Id, string Name) : IRequest<Result<bool>>;