namespace TicketFly.Application.AppNotifications.Queries.GetById;
public record GetAppNotificationsByIdQuery(Guid Id) : IRequest<Result<AppNotificationDto>>;