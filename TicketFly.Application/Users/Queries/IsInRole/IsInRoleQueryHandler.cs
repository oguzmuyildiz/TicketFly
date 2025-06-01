using TicketFly.Application.Common.Intefaces.Data;

namespace TicketFly.Application.Users.Queries.IsInRole;

public class IsInRoleQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<IsInRoleQuery, Result<bool>>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<bool>> Handle(IsInRoleQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Users
            .Where(t => t.Id == request.UserId)
            .AnyAsync(t => t.UserRoles.Any(ur => ur.Role.Name == request.Role), cancellationToken);

        return _mapper.Map<bool>(result);
    }
}