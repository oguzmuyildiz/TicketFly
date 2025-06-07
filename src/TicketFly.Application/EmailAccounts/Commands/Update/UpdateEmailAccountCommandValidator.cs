namespace TicketFly.Application.EmailAccounts.Commands.Update;
internal sealed class UpdateEmailAccountCommandValidator : AbstractValidator<UpdateEmailAccountCommand>
{
    private readonly IAppDbContext _context;
    public UpdateEmailAccountCommandValidator(IAppDbContext context)
    {
        _context = context;

        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200)
            .MustAsync(CheckUniqueEmail)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

        RuleFor(v => v.ApiKey)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Host)
            .NotEmpty()
            .MaximumLength(200);
    }

    public async Task<bool> CheckUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return !await _context.EmailAccounts
            .AnyAsync(l => l.Email == email, cancellationToken);
    }
}
