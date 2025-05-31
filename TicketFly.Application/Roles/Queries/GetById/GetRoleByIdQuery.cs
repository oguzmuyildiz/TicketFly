using TicketFly.Domain.Entities;

namespace TicketFly.Application.Roles.Queries.GetById;
public record GetRoleByIdQuery(Guid Id) : IRequest<Result<Role>>;