namespace TicketFly.Application.TicketAlarmRules.Commands.Create;
internal sealed class CreateTicketAlarmRuleCommandValidator : AbstractValidator<CreateTicketAlarmRuleCommand>
{
    public CreateTicketAlarmRuleCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);
    }

}
