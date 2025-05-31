namespace TicketFly.Application.Users.Commands.Register;

public record RegisterUserCommand(string Email, string UserName, string FirstName, string LastName, string Password) : IRequest<Result<string>>;