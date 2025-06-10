namespace TicketFly.Application.AppNotifications.Commands.Create;
internal sealed class CreateAppNotificationCommandValidator : AbstractValidator<CreateAppNotificationCommand>
{
    public CreateAppNotificationCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Content)
            .NotEmpty()
            .MaximumLength(200);
    }
}
