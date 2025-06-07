using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Domain.Models;

namespace TicketFly.Application.Users.Commands.Register;
public class RegisterUserCommandHandler(
    IAppDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider,
    IUserContext userContext) : IRequestHandler<RegisterUserCommand, Result<TokenModel>>
{

    public async Task<Result<TokenModel>> Handle(RegisterUserCommand command, 
        CancellationToken cancellationToken)
    {
        var entity = new User
        {
            Email = command.Email,
            PasswordHash = passwordHasher.Hash(command.Password),
            FirstName = command.FirstName,
            LastName = command.LastName,
            UserName = command.UserName
        };
        context.Users.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        TokenModel token = tokenProvider.Create(entity, userContext.IpAddress);

        await context.SaveChangesAsync(cancellationToken);

        return token;
    }
}
