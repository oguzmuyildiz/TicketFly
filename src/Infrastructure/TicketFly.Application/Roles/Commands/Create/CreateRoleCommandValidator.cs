namespace TicketFly.Application.Roles.Commands.Create;
internal sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IAppDbContext _context;
    public CreateRoleCommandValidator(IAppDbContext context)
    {
        _context = context;
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(CheckUniqueName)
                .WithMessage("'{PropertyName}'already exists.")
                .WithErrorCode("Unique");
    }
    public async Task<bool> CheckUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _context.Roles
            .AnyAsync(l => l.Name == name, cancellationToken);
    }
}
