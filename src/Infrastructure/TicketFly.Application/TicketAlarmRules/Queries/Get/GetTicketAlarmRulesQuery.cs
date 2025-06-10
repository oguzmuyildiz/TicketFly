namespace TicketFly.Application.TicketAlarmRules.Queries.Get;
public record GetTicketAlarmRulesQuery : IRequest<Result<IEnumerable<TicketAlarmRuleDto>>>;