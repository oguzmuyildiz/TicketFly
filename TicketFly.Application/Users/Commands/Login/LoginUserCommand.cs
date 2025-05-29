namespace TicketFly.Application.Users.Commands.Login;

public record LoginUserCommand (string Email, string Password) : IRequest<string>;
