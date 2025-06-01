namespace TicketFly.Application.Users.Queries.IsInRole;
public record IsInRoleQuery(Guid UserId, string Role) : IRequest<Result<bool>>;