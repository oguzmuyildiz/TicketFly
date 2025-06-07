namespace TicketFly.Application.EmailAccounts.Queries.GetByOrganizationId;
public record GetEmailAccountsByOrganizationIdQuery(Guid OrganizationId) : IRequest<Result<IEnumerable<EmailAccountDto>>>;