namespace TicketFly.Application.Users.Queries.IsInRole;
public class IsInRoleQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<IsInRoleQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(IsInRoleQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await context.Users
            .Where(t => t.Id == request.UserId)
            .AnyAsync(t => t.UserRoles.Any(ur => ur.Role.Name == request.Role), cancellationToken);

        return mapper.Map<bool>(result);
    }
}