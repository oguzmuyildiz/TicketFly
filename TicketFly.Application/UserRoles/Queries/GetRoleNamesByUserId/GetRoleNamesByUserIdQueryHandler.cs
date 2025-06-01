using TicketFly.Application.Common.Intefaces.Data;

namespace TicketFly.Application.UserRoles.Queries.GetRoleNamesByUserId;

public class GetRoleNamesByUserIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetRoleNamesByUserIdQuery, Result<IEnumerable<string>>>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<IEnumerable<string>>> Handle(GetRoleNamesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userRoles = await _context.UserRoles
            .Include(x => x.Role)
               .Where(t => t.UserId == request.UserId)
               .Select(x => x.Role.Name)
               .ToListAsync();

        if (userRoles is null)
        {
            return Result.Failure<IEnumerable<string>>(Error.NotFound("User Roles not found", $"User Roles with UserID {request.UserId} not found."));
        }
        return userRoles;
    }
}