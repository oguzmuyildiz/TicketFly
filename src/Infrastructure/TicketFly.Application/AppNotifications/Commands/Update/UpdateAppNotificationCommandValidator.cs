namespace TicketFly.Application.AppNotifications.Commands.Update;
internal sealed class UpdateAppNotificationCommandValidator : AbstractValidator<UpdateAppNotificationCommand>
{
    public UpdateAppNotificationCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Content)
            .NotEmpty()
            .MaximumLength(200);
    }
}
