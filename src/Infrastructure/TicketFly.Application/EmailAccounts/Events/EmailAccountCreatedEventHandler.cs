using Microsoft.Extensions.Logging;
using TicketFly.Domain.Events;

namespace TicketFly.Application.EmailAccounts.Events;
public class EmailAccountCreatedEventHandler(ILogger<EmailAccountCreatedEventHandler> logger) : INotificationHandler<EmailAccountCreatedEvent>
{
    public async Task Handle(EmailAccountCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Triggered: {DomainEvent} {Organization} {Email}", notification.GetType().Name, notification.Item.Organization.Name, notification.Item.Email);

        //await bus.Publish(new CreateCronJobCommand(notification.Item.OrganizationId, 
        //    notification.Item.Host,
        //    notification.Item.Email,
        //    notification.Item.ApiKey,
        //    notification.Item.Port), cancellationToken);
    }
}
