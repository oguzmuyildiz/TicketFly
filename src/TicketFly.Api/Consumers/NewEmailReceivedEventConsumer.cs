using MassTransit;
using RabbitMQ.Shared.Models.EmailParser.Events;

namespace TicketFly.Api.Consumers;

public class NewEmailReceivedEventConsumer(ILogger<NewEmailReceivedEventConsumer> logger) : IConsumer<NewEmailReceivedEvent>
{

    public Task Consume(ConsumeContext<NewEmailReceivedEvent> context)
    {
        //Create New Ticket
        //Send Notification

        var From = context.Message.From;
        var Body = context.Message.Body;
        var Subject = context.Message.Subject;
        var OrganizationId = context.Message.OrganizationId;
        var To = context.Message.To;

        logger.LogInformation("New Email Received, From: {From}, Message: {Message}", From, context.Message);

        return Task.CompletedTask;
    }
}