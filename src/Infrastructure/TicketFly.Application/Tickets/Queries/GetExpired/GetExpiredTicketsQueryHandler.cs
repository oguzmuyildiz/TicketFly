namespace TicketFly.Application.Tickets.Queries.GetExpired;
public class GetExpiredTicketsQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetExpiredTicketsQuery, Result<IEnumerable<TicketDto>>>
{
    public async Task<Result<IEnumerable<TicketDto>>> Handle(GetExpiredTicketsQuery request, 
        CancellationToken cancellationToken)
    {
        //TODO: Create a specification for expired tickets
        return await context.Tickets
            .Where(t => t.Status == Domain.Enums.TicketStatus.New)
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<TicketDto>(mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
