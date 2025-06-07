namespace TicketFly.Application.UserRoles.Queries.GetRolesByUserId;
public record GetRolesByUserIdQuery(Guid UserId) : IRequest<Result<IEnumerable<Role>>>;