namespace TicketFly.Application.AppNotifications.Queries.GetByUserId;
public class GetAppNotificationsByUserIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetAppNotificationsByUserIdQuery, Result<IEnumerable<AppNotificationDto>>>
{
    public async Task<Result<IEnumerable<AppNotificationDto>>> Handle(GetAppNotificationsByUserIdQuery request, CancellationToken cancellationToken)
    {

        var user = await context.Users
            .Where(u => u.Id == request.UserId)
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (user is null)
        {
            return Result.Failure<IEnumerable<AppNotificationDto>>(Error.NotFound("User not found", $"User with ID {request.UserId} not found."));
        }

        //TODO: Check performance of this query, consider using a different approach
        var userRoles = user.UserRoles.Select(x => x.RoleId).ToList();
        var result = await context.AppNotifications
               .Where(t => t.UserId == request.UserId ||
                    (t.OrganizationId == user.OrganizationId && t.RoleId == null) ||
                    (t.OrganizationId == user.OrganizationId && userRoles.Any(x => x == t.RoleId)))
               .ProjectTo<AppNotificationDto>(mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<IEnumerable<AppNotificationDto>>(Error.NotFound("AppNotification not found", $"AppNotification with UserID {request.UserId} not found."));
        }

        return result;
    }
}