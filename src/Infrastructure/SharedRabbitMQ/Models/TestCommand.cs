using MassTransit;
namespace TicketFly.Shared.Models;

[EntityName("testcommand.testcommand")]
public record TestCommand(string TestText)
{
    public string TestText { get; protected set; } = TestText;
}
