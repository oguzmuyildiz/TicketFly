namespace TicketFly.Application.UserRoles.Queries.GetRoleNamesByUserId;
public class GetRoleNamesByUserIdQueryHandler(IAppDbContext context) : 
    IRequestHandler<GetRoleNamesByUserIdQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string>>> Handle(GetRoleNamesByUserIdQuery request, 
        CancellationToken cancellationToken)
    {
        var userRoles = await context.UserRoles
            .Include(x => x.Role)
               .Where(t => t.UserId == request.UserId)
               .Select(x => x.Role.Name)
               .ToListAsync();

        if (userRoles is null)
        {
            return Result.Failure<IEnumerable<string>>(
                Error.NotFound("User Roles not found", $"User Roles with UserID {request.UserId} not found."));
        }
        return userRoles;
    }
}