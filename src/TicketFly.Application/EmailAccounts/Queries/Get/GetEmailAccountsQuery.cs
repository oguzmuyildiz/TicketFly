namespace TicketFly.Application.EmailAccounts.Queries.Get;
public record GetEmailAccountsQuery() : IRequest<Result<IEnumerable<EmailAccountDto>>>;