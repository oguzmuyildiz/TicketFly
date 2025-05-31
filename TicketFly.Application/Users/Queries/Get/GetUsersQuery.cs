namespace TicketFly.Application.Users.Queries.Get;
public record GetUsersQuery : IRequest<Result<IEnumerable<UserDto>>>;