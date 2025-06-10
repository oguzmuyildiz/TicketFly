using TicketFly.Domain.Enums;

namespace TicketFly.Application.TicketAlarmRules.Commands.Update;
public record UpdateTicketAlarmRuleCommand(
    Guid Id,
    Guid OrganizationId,
    string Name,
    string Description,
    int Duration,
    TimePeriods Period) : IRequest<Result<bool>>;