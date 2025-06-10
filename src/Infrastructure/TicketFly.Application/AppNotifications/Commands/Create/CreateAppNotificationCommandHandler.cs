namespace TicketFly.Application.AppNotifications.Commands.Create;
public class CreateAppNotificationCommandHandler(ILogger<CreateAppNotificationCommandHandler> logger, IAppDbContext context) : IRequestHandler<CreateAppNotificationCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateAppNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = new AppNotification
        {
            Title = request.Title,
            Content = request.Content,
            Link = request.Link,
            IsRead = false,
            OrganizationId = request.OrganizationId,
            RoleId = request.RoleId,
            UserId = request.UserId,            
        };

        context.AppNotifications.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("AppNotification created with ID: {Id}", entity.Id);

        return entity.Id;
    }
}