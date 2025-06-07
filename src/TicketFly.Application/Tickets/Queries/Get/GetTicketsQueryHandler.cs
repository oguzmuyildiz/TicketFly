namespace TicketFly.Application.Tickets.Queries.Get;
public class GetTicketsQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetTicketsQuery, Result<IEnumerable<TicketDto>>>
{
    public async Task<Result<IEnumerable<TicketDto>>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        return await context.Tickets
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<TicketDto>(mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}