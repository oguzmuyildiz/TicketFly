namespace TicketFly.Application.Clients.Commands.Update;
internal sealed class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    private readonly IAppDbContext _context;
    public UpdateClientCommandValidator(IAppDbContext context)
    {
        _context = context;
        RuleFor(v => v.Id)
            .NotEmpty();

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

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