namespace TicketFly.Application.AppNotifications.Queries.GetById;
public class GetAppNotificationsByIdQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetAppNotificationsByIdQuery, Result<AppNotificationDto>>
{
    public async Task<Result<AppNotificationDto>> Handle(GetAppNotificationsByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await context.AppNotifications
               .SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<AppNotificationDto>(
                Error.NotFound("AppNotification not found", $"AppNotification with ID {request.Id} not found."));
        }

        return mapper.Map<AppNotificationDto>(result);
    }
}