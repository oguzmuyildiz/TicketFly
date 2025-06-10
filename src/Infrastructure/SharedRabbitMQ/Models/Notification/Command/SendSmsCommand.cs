using MassTransit;

namespace TicketFly.Shared.Models.Notification.Command;

[EntityName("notification.sendsmscommand")]
public record SendSmsCommand(string Phone, string Text);