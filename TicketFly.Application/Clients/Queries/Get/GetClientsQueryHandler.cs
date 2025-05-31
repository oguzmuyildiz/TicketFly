using AutoMapper.QueryableExtensions;
using TicketFly.Application.Common.Intefaces.Data;

namespace TicketFly.Application.Clients.Queries.Get;

public class GetClientsQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetClientsQuery, Result<IEnumerable<ClientDto>>>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<IEnumerable<ClientDto>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Clients
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
