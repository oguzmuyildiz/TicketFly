namespace TicketFly.Domain.Dtos;
public record TicketAlarmDto(Guid Id, Guid TicketId, Guid RuleId, string Title);