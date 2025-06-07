namespace TicketFly.Application.Clients.Queries.Get;
public class GetClientsQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetClientsQuery, Result<IEnumerable<ClientDto>>>
{
    public async Task<Result<IEnumerable<ClientDto>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        return await context.Clients
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<ClientDto>(mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
