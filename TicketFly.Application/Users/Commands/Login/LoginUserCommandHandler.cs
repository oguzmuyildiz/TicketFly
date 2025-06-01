using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;
using TicketFly.Domain.Models;

namespace TicketFly.Application.Users.Commands.Login;

public class LoginUserCommandHandler(
    IAppDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, Result<TokenModel>>
{

    public async Task<Result<TokenModel>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User user = await context.Users
            .Include(x=>x.UserRoles)
            .ThenInclude(x => x.Role)
            .SingleAsync(u => u.Email == command.Email, cancellationToken);

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<TokenModel>(Error.Unauthorized("Invalid email or password", "The provided email or password is incorrect."));
        }
        
        TokenModel token = tokenProvider.Create(user);
        await context.SaveChangesAsync(cancellationToken);

        return token;
    }
}
