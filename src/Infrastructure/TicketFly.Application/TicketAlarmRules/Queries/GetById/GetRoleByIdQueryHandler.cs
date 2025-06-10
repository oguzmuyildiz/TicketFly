namespace TicketFly.Application.TicketAlarmRules.Queries.GetById;
public class GetTicketAlarmRuleByIdQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetTicketAlarmRuleByIdQuery, Result<TicketAlarmRuleDto>>
{
    public async Task<Result<TicketAlarmRuleDto>> Handle(GetTicketAlarmRuleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await context.TicketAlarmRules
               .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<TicketAlarmRuleDto>(Error.NotFound("TicketAlarmRule not found", 
                $"TicketAlarmRule with ID {request.Id} not found."));
        }
        return mapper.Map<TicketAlarmRuleDto>(result);
    }
}