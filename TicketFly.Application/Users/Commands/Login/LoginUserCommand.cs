using TicketFly.Domain.Models;

namespace TicketFly.Application.Users.Commands.Login;

public record LoginUserCommand (string Email, string Password) : IRequest<Result<TokenModel>>;
