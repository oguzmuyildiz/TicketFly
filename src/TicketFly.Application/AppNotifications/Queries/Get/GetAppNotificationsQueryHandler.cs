namespace TicketFly.Application.AppNotifications.Queries.Get;
public class GetAppNotificationsQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetAppNotificationsQuery, Result<IEnumerable<AppNotificationDto>>>
{
    public async Task<Result<IEnumerable<AppNotificationDto>>> Handle(GetAppNotificationsQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await context.AppNotifications
               .AsNoTracking()
               .ProjectTo<AppNotificationDto>(mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<IEnumerable<AppNotificationDto>>(
                Error.NotFound("AppNotification not found", $"AppNotification not found."));
        }

        return result;
    }
}
}