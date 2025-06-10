using TicketFly.Domain.Events;

namespace TicketFly.Application.Tickets.Events;
public class TicketCompletedEventHandler(ILogger<TicketCompletedEventHandler> logger) : 
    INotificationHandler<TicketCompletedEvent>
{

    public async Task Handle(TicketCompletedEvent notification, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Triggered: {DomainEvent} {Id}", notification.GetType().Name, notification.Item.Id);
    }
}