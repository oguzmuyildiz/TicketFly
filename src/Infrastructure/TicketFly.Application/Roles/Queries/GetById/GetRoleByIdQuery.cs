namespace TicketFly.Application.Roles.Queries.GetById;
public record GetRoleByIdQuery(Guid Id) : IRequest<Result<RoleDto>>;