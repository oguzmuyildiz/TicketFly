using TicketFly.Domain.Common;

namespace TicketFly.Application.Users.Commands.Login;

public record LoginUserCommand (string Email, string Password) : IRequest<Result<string>>;
