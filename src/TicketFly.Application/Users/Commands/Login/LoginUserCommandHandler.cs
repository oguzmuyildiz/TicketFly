using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Domain.Models;

namespace TicketFly.Application.Users.Commands.Login;
public class LoginUserCommandHandler(
    IAppDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider,
    IUserContext userContext) : IRequestHandler<LoginUserCommand, Result<TokenModel>>
{
    public async Task<Result<TokenModel>> Handle(LoginUserCommand command, 
        CancellationToken cancellationToken)
    {
        User user = await context.Users
            .Include(x=>x.UserRoles)
            .ThenInclude(x => x.Role)
            .SingleAsync(u => u.Email == command.Email, cancellationToken);

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<TokenModel>(
                Error.Unauthorized("Invalid email or password", "The provided email or password is incorrect."));
        }

        var ActiveTokens = context.RefreshTokens.Where(x => x.UserId==user.Id && x.IsActive);
        foreach (var RefreshTokenItem in ActiveTokens)
        {
            RefreshTokenItem.IsActive = false;
        }

        TokenModel token = tokenProvider.Create(user, userContext.IpAddress);
        await context.SaveChangesAsync(cancellationToken);

        return token;
    }
}
