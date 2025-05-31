namespace TicketFly.Application.UserRoles.Commands.Create;

public record CreateUserRoleCommand(Guid UserId, Guid RoleId) : IRequest<Result<Guid>>;