using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;
using TicketFly.Domain.Models;

namespace TicketFly.Application.Users.Commands.Register;

public class RegisterUserCommandHandler(
    IAppDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider,
    IUserContext userContext) : IRequestHandler<RegisterUserCommand, Result<TokenModel>>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IAppDbContext _context = context;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public async Task<Result<TokenModel>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var entity = new User
        {
            Email = command.Email,
            PasswordHash = _passwordHasher.Hash(command.Password),
            FirstName = command.FirstName,
            LastName = command.LastName,
            UserName = command.UserName
        };
        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        TokenModel token = _tokenProvider.Create(entity, _userContext.IpAddress);

        await context.SaveChangesAsync(cancellationToken);

        return token;
    }
}
