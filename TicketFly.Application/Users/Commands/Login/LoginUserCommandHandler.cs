using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Users.Commands.Login;

public class LoginUserCommandHandler(
    IAppDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, Result<string>>
{

    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User user = await context.Users
            .Include(x=>x.UserRoles)
            .ThenInclude(x => x.Role)
            .SingleAsync(u => u.Email == command.Email, cancellationToken);

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<string>(Error.Unauthorized("Invalid email or password", "The provided email or password is incorrect."));
        }

        string token = tokenProvider.Create(user);

        return token;
    }
}
