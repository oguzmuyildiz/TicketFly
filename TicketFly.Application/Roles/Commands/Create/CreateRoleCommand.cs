using TicketFly.Application.Common.Security;
using TicketFly.Domain.Constants;

namespace TicketFly.Application.Roles.Commands.Create;

public record CreateRoleCommand(string Name) : IRequest<Result<Guid>>;