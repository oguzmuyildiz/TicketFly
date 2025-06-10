using TicketFly.Domain.Enums;

namespace TicketFly.Application.EmailAccounts.Commands.Create;

public record CreateEmailAccountCommand(Guid OrganizationId, string Host, string Email, string ApiKey, MailProviders Provider, int Port, bool IsActive) : IRequest<Result<Guid>>;