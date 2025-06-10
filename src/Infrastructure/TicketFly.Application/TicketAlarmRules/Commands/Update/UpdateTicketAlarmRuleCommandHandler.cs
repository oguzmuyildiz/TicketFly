namespace TicketFly.Application.TicketAlarmRules.Commands.Update;
public class UpdateTicketAlarmRuleCommandHandler(IAppDbContext context) : 
    IRequestHandler<UpdateTicketAlarmRuleCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateTicketAlarmRuleCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = context.TicketAlarmRules.FirstOrDefault(x => x.Id == request.Id);

        if (entity is null)
        {
            return Result.Failure<bool>(
                Error.NotFound("TicketAlarmRule not found", $"TicketAlarmRule with ID {request.Id} not found."));
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Duration = request.Duration;
        entity.Period = request.Period;
        entity.OrganizationId = request.OrganizationId;
        
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
