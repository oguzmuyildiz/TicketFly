namespace TicketFly.Application.UserRoles.Queries.GetRoleNamesByUserId;
public record GetRoleNamesByUserIdQuery(Guid UserId) : IRequest<Result<IEnumerable<string>>>;