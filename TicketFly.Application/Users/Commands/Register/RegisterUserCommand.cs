using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Domain.Common;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Users.Commands.Register;

public record RegisterUserCommand(string Email, string UserName, string FirstName, string LastName, string Password) : IRequest<Result<string>>;