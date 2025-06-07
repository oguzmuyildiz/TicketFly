namespace TicketFly.Application.UserRoles.Commands.Create;
internal sealed class CreateUserRoleCommandValidator : 
    AbstractValidator<CreateUserRoleCommand>
{
    public CreateUserRoleCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty();
        RuleFor(v => v.RoleId)
            .NotEmpty();
    }
}
