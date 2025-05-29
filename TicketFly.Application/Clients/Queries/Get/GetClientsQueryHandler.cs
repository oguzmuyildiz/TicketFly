using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Dtos;

namespace TicketFly.Application.Clients.Queries.Get;

public class GetClientsQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetClientsQuery, IEnumerable<ClientDto>>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Clients
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
