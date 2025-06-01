using Microsoft.EntityFrameworkCore;
using TicketFly.Application.Common.Intefaces.Data;

namespace TicketFly.Application.Users.Commands.Register;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IAppDbContext _context;
    public RegisterUserCommandValidator(IAppDbContext context)
    {
        _context = context;
        RuleFor(c => c.FirstName)
            .NotEmpty();

        RuleFor(c => c.LastName)
            .NotEmpty();

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(CheckUniqueEmail)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

        RuleFor(c => c.UserName)
            .NotEmpty()
            .MinimumLength(6)
            .MustAsync(CheckUniqueUsername)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique"); ;

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(8);
    }

    public async Task<bool> CheckUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return !await _context.Users
            .AnyAsync(l => l.Email == email, cancellationToken);
    }
    
    public async Task<bool> CheckUniqueUsername(string username, CancellationToken cancellationToken)
    {
        return !await _context.Users
            .AnyAsync(l => l.UserName == username, cancellationToken);
    }
}