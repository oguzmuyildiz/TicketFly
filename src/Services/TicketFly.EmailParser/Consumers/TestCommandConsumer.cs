using MassTransit;
using TicketFly.Shared.Models;

namespace TicketFly.EmailParser.Consumers;
public class TestCommandConsumer(ILogger<TestCommandConsumer> logger) : IConsumer<TestCommand>
{
    private readonly ILogger<TestCommandConsumer> _logger = logger;

    public Task Consume(ConsumeContext<TestCommand> context)
    {
        var Text = context.Message.TestText;
        _logger.LogInformation("EmailParser.TestCommand with message: {Text}", Text);

        return Task.CompletedTask;
    }
}