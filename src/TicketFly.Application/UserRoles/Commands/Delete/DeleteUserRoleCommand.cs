namespace TicketFly.Application.UserRoles.Commands.Delete;
public record DeleteUserRoleCommand(Guid Id) : IRequest<Result<bool>>;