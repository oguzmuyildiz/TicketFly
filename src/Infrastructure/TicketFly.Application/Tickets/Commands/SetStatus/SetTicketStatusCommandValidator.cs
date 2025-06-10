namespace TicketFly.Application.Tickets.Commands.SetStatus;
internal sealed class SetTicketStatusCommandValidator : 
    AbstractValidator<SetTicketStatusCommand>
{
    private readonly IAppDbContext _context;
    public SetTicketStatusCommandValidator(IAppDbContext context)
    {
        _context = context;
        RuleFor(v => v.Id)
            .NotEmpty();

        RuleFor(v => v.TicketStatus)
            .NotEmpty();
    }
}
