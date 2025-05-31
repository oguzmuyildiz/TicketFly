namespace TicketFly.Application.Users.Queries.GetByEmail;
public record GetUserByEmailQuery(string Email) : IRequest<Result<UserDto>>;