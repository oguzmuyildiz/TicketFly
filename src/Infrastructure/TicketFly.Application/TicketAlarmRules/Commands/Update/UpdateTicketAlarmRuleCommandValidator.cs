namespace TicketFly.Application.TicketAlarmRules.Commands.Update;
internal sealed class UpdateTicketAlarmRuleCommandValidator : 
    AbstractValidator<UpdateTicketAlarmRuleCommand>
{
    public UpdateTicketAlarmRuleCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}
