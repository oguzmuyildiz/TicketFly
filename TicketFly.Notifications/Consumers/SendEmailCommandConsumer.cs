using MassTransit;
using RabbitMQ.Shared.Models.Notification.Command;

namespace TicketFly.Notifications.Consumers;

public class SendEmailCommandConsumer(ILogger<SendSmsCommandConsumer> logger) : IConsumer<SendEmailCommand>
{
    public Task Consume(ConsumeContext<SendEmailCommand> context)
    {
        var Subject = context.Message.Subject;
        var To = context.Message.To;
        logger.LogInformation("Send Email to {To} with Subject: {Subject}", To, Subject);
        return Task.CompletedTask;
    }
}
