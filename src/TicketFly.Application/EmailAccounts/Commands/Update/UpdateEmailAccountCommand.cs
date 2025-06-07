using TicketFly.Domain.Enums;

namespace TicketFly.Application.EmailAccounts.Commands.Update;
public record UpdateEmailAccountCommand(Guid Id, Guid OrganizationId, string Host, string Email, string ApiKey, MailProviders Provider, int Port, bool IsActive) : IRequest<Result<bool>>;