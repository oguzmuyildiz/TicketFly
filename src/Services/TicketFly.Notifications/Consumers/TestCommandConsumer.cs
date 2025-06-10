using MassTransit;
using TicketFly.Shared.Models;

namespace TicketFly.Notifications.Consumers;
public class TestCommandConsumer(ILogger<TestCommandConsumer> logger) : IConsumer<TestCommand>
{
    private readonly ILogger<TestCommandConsumer> _logger = logger;

    public Task Consume(ConsumeContext<TestCommand> context)
    {
        var Text = context.Message.TestText;
        _logger.LogInformation("Notifications.TestCommand with message: {Text}", Text);

        return Task.CompletedTask;
    }
}