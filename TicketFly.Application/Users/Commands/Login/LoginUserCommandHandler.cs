using Microsoft.EntityFrameworkCore;
using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Users.Commands.Login;

public class LoginUserCommandHandler(
    IAppDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, string>
{

    public async Task<string> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User user = await context.Users
            .AsNoTracking()
            .SingleAsync(u => u.Email == command.Email, cancellationToken);

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        string token = tokenProvider.Create(user);

        return token;
    }
}
