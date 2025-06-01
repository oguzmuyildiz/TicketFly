using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Models;

namespace TicketFly.Application.Users.Commands.Refresh;

public class RefreshTokenCommandHandler(
    IAppDbContext context,
    ITokenProvider tokenProvider) : IRequestHandler<RefreshTokenCommand, Result<TokenModel>>
{

    public async Task<Result<TokenModel>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var user = context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == command.RefreshToken));
        
        if (user == null)
        {
            return Result.Failure<TokenModel>(Error.Unauthorized("Invalid RefreshToken", "The provided RefreshToken is incorrect."));
        }

        var refreshToken = user.RefreshTokens.Single(x => x.Token == command.RefreshToken);

        if (refreshToken==null || !refreshToken.IsActive || refreshToken.IsExpired)
        {
            return Result.Failure<TokenModel>(Error.Unauthorized("Invalid RefreshToken", "The provided RefreshToken is incorrect."));
        }

        refreshToken.IsActive = false;
        TokenModel token = tokenProvider.Create(user);
        await context.SaveChangesAsync(cancellationToken);

        return token;
    }

}
