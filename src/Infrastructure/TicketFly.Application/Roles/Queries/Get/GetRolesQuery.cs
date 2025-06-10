namespace TicketFly.Application.Roles.Queries.Get;
public record GetRolesQuery : IRequest<Result<IEnumerable<RoleDto>>>;