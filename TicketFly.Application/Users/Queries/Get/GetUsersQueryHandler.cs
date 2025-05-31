using AutoMapper.QueryableExtensions;
using TicketFly.Application.Common.Intefaces.Data;

namespace TicketFly.Application.Users.Queries.Get;

public class GetUsersQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetUsersQuery, Result<IEnumerable<UserDto>>>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
