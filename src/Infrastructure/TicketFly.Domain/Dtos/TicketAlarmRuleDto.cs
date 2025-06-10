namespace TicketFly.Domain.Dtos;
public record TicketAlarmRuleDto(Guid Id, Guid OrganizationId, string Name, string Description, int Duration, TimePeriods Period);
