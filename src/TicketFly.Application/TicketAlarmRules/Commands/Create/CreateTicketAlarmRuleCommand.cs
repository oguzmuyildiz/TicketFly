using TicketFly.Domain.Enums;

namespace TicketFly.Application.TicketAlarmRules.Commands.Create;
public record CreateTicketAlarmRuleCommand(
    Guid OrganizationId, 
    string Name, 
    string Description, 
    int Duration, 
    TimePeriods Period) : IRequest<Result<Guid>>;