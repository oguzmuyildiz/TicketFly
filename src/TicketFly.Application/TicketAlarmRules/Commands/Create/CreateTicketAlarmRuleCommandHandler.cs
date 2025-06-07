namespace TicketFly.Application.TicketAlarmRules.Commands.Create;
public class CreateTicketAlarmRuleCommandHandler(IAppDbContext context) : 
    IRequestHandler<CreateTicketAlarmRuleCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateTicketAlarmRuleCommand request, CancellationToken cancellationToken)
    {
        var entity = new TicketAlarmRule
        {
            OrganizationId = request.OrganizationId,
            Description = request.Description,
            Duration = request.Duration,
            Period = request.Period,
            Name = request.Name
        };

        context.TicketAlarmRules.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
