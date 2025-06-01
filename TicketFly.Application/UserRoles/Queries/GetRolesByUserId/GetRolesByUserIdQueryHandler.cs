using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.UserRoles.Queries.GetRolesByUserId;

public class GetRolesByUserIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetRolesByUserIdQuery, Result<IEnumerable<Role>>>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<IEnumerable<Role>>> Handle(GetRolesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userRoles = await _context.UserRoles
            .Include(x => x.Role)
               .Where(t => t.UserId == request.UserId)
               .Select(x => x.Role)
               .ToListAsync();

        if (userRoles is null)
        {
            return Result.Failure<IEnumerable<Role>>(Error.NotFound("User Roles not found", $"User Roles with UserID {request.UserId} not found."));
        }
        return userRoles;
    }
}