using AutoMapper.QueryableExtensions;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Roles.Queries.Get;

public class GetRolesQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetRolesQuery, Result<IEnumerable<Role>>>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<IEnumerable<Role>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ToListAsync(cancellationToken);
    }
}
