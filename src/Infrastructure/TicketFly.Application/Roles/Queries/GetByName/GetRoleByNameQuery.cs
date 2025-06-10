namespace TicketFly.Application.Roles.Queries.GetByName;
public record GetRoleByNameQuery(string Name) : IRequest<Result<RoleDto>>;