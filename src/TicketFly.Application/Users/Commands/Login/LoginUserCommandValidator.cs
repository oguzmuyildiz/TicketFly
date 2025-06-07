namespace TicketFly.Application.Users.Commands.Login;
internal sealed class LoginUserCommandValidator : 
    AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}