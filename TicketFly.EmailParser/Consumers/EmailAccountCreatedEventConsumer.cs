using MassTransit;
using RabbitMQ.Shared.Models.TicketApi.Events;

namespace TicketFly.EmailParser.Consumers;

public class EmailAccountCreatedEventConsumer(ILogger<EmailAccountCreatedEventConsumer> logger) : IConsumer<EmailAccountCreatedEvent>
{

    public Task Consume(ConsumeContext<EmailAccountCreatedEvent> context)
    {
        //Create Cron Job to check for new emails every 5 minutes

        var ApiKey = context.Message.ApiKey;
        var Email = context.Message.Email;
        var Host = context.Message.Host;
        var OrganizationId = context.Message.OrganizationId;
        var Port = context.Message.Port;

        logger.LogInformation("Email Account Created Event Consumer , Email: {Email}", Email, context.Message);

        return Task.CompletedTask;
    }
}