using TicketFly.Domain.Events;

namespace TicketFly.Application.Clients.Events;
public class ClientCreatedEventHandler(ILogger<ClientCreatedEventHandler> logger) : INotificationHandler<ClientCreatedEvent>
{
    public Task Handle(ClientCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Triggered: {DomainEvent} {ClientEmail}", notification.GetType().Name, notification.Item.Email);
        return Task.CompletedTask;
    }
}
