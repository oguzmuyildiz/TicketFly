namespace TicketFly.Application.AppNotifications.Queries.Get;
public record GetAppNotificationsQuery() : IRequest<Result<IEnumerable<AppNotificationDto>>>;