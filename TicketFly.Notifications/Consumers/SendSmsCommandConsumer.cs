using MassTransit;
using RabbitMQ.Shared.Models.Notification.Command;

namespace TicketFly.Notifications.Consumers;
public class SendSmsCommandConsumer : IConsumer<SendSmsCommand>
{
    private readonly ILogger<SendSmsCommandConsumer> _logger;

    public SendSmsCommandConsumer(ILogger<SendSmsCommandConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SendSmsCommand> context)
    {
        var Phone = context.Message.Phone;
        var Text = context.Message.Text;
        _logger.LogInformation("Send SMS to {Phone} with message: {Text}", Phone, Text);

        return Task.CompletedTask;
    }
}