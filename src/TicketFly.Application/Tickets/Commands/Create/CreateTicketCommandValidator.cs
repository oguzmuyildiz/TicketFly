namespace TicketFly.Application.Tickets.Commands.Create;
internal sealed class CreateTicketCommandValidator : 
    AbstractValidator<CreateTicketCommand>
{
    public CreateTicketCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Content)
            .NotEmpty();
    }
}
