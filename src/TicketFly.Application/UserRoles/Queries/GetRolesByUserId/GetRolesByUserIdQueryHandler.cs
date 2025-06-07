namespace TicketFly.Application.UserRoles.Queries.GetRolesByUserId;
public class GetRolesByUserIdQueryHandler(IAppDbContext context) : 
    IRequestHandler<GetRolesByUserIdQuery, Result<IEnumerable<Role>>>
{
    public async Task<Result<IEnumerable<Role>>> Handle(GetRolesByUserIdQuery request, 
        CancellationToken cancellationToken)
    {
        var userRoles = await context.UserRoles
            .Include(x => x.Role)
               .Where(t => t.UserId == request.UserId)
               .Select(x => x.Role)
               .ToListAsync();

        if (userRoles is null)
        {
            return Result.Failure<IEnumerable<Role>>(
                Error.NotFound("User Roles not found", $"User Roles with UserID {request.UserId} not found."));
        }
        return userRoles;
    }
}