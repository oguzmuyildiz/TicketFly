namespace TicketFly.Application.AppNotifications.Commands.Update;
public class UpdateAppNotificationCommandHandler(ILogger logger, IAppDbContext context) : IRequestHandler<UpdateAppNotificationCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateAppNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = context.AppNotifications.FirstOrDefault(x => x.Id == request.Id);

        if (entity is null)
        {
            return Result.Failure<bool>(Error.NotFound("AppNotification not found", $"AppNotification with ID {request.Id} not found."));
        }

        entity.Content = request.Content;

        context.AppNotifications.Update(entity);
        await context.SaveChangesAsync(cancellationToken);

        logger.Information("AppNotification updated with ID: {Id}", entity.Id);

        return true;
    }
}