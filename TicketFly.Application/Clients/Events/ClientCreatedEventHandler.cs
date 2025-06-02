using Microsoft.Extensions.Logging;
using TicketFly.Domain.Events;

namespace TicketFly.Application.Clients.Events;

public class ClientCreatedEventHandler(ILogger<ClientCreatedEventHandler> logger) : INotificationHandler<ClientCreatedEvent>
{
    private readonly ILogger<ClientCreatedEventHandler> _logger = logger;

    public Task Handle(ClientCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event Triggered: {DomainEvent} {ClientEmail}", notification.GetType().Name, notification.Item.Email);
        return Task.CompletedTask;
    }
}
