using MassTransit;

namespace TicketFly.Shared.Models.EmailParser.Commands;

[EntityName("emailparser.createcronjobcommand")]
public record CreateCronJobCommand(
    Guid OrganizationId,
    string Host,
    string Email,
    string ApiKey,
    int Port)
{
    public Guid OrganizationId { get; private set; } = OrganizationId;
    public string Host { get; private set; } = Host;
    public string Email { get; private set; } = Email;
    public string ApiKey { get; private set; } = ApiKey;
    public int Port { get; private set; } = Port;
}