namespace TicketFly.Application.EmailAccounts.Queries.GetById;
public record GetEmailAccountsByIdQuery(Guid Id) : IRequest<Result<EmailAccountDto>>;