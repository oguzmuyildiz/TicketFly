namespace TicketFly.Application.AppNotifications.Queries.GetByUserId;
public record GetAppNotificationsByUserIdQuery(Guid UserId) : IRequest<Result<IEnumerable<AppNotificationDto>>>;