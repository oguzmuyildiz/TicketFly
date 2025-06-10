namespace TicketFly.Application.Clients.Commands.Create;
internal sealed class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    private readonly IAppDbContext _context;
    public CreateClientCommandValidator(IAppDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200)
            .MustAsync(CheckUniqueEmail)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

        RuleFor(v => v.Domain)
            .NotEmpty()
            .MaximumLength(200);
    }
    public async Task<bool> CheckUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return !await _context.Clients
            .AnyAsync(l => l.Email == email, cancellationToken);
    }
}
