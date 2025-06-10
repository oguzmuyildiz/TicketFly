namespace TicketFly.Application.EmailAccounts.Queries.GetByOrganizationId;
public class GetEmailAccountsByOrganizationIdQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetEmailAccountsByOrganizationIdQuery, Result<IEnumerable<EmailAccountDto>>>
{
    public async Task<Result<IEnumerable<EmailAccountDto>>> Handle(GetEmailAccountsByOrganizationIdQuery request, 
        CancellationToken cancellationToken)
    {
        return await context.EmailAccounts
            .Where(t => t.OrganizationId == request.OrganizationId)
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<EmailAccountDto>(mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
