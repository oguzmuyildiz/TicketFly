namespace TicketFly.Application.Users.Queries.GetById;
public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;