namespace TicketFly.Application.Organizations.Commands.Update;
internal sealed class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    public UpdateOrganizationCommandValidator()
    {
        RuleFor(v => v.Name)
             .NotEmpty()
             .MaximumLength(200);
    }
}
