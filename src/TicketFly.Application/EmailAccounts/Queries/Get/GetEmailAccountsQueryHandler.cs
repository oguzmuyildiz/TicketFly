namespace TicketFly.Application.EmailAccounts.Queries.Get;
public class GetEmailAccountsQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetEmailAccountsQuery, Result<IEnumerable<EmailAccountDto>>>
{
    public async Task<Result<IEnumerable<EmailAccountDto>>> Handle(GetEmailAccountsQuery request, 
        CancellationToken cancellationToken)
    {
        return await context.EmailAccounts
            .OrderByDescending(t => t.Created)
            .AsNoTracking()
            .ProjectTo<EmailAccountDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}