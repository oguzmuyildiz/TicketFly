using TicketFly.Application.Common.Intefaces.Data;

namespace TicketFly.Application.Roles.Commands.Update;
public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    private readonly IAppDbContext _context;
    public UpdateRoleCommandValidator(IAppDbContext context)
    {
        _context = context;
        RuleFor(v => v.Id)
            .NotEmpty();

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
