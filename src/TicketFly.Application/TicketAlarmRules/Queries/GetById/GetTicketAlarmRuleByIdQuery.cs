namespace TicketFly.Application.TicketAlarmRules.Queries.GetById;
public record GetTicketAlarmRuleByIdQuery(Guid Id) : IRequest<Result<TicketAlarmRuleDto>>;