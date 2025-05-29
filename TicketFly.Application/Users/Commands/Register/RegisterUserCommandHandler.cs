using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Users.Commands.Register;

public class RegisterUserCommandHandler(
    IAppDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IAppDbContext _context = context;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public async Task<string> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
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
        string token = _tokenProvider.Create(entity);
        return token;
    }
}
