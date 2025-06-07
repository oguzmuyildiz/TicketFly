namespace TicketFly.Application.TicketAlarmRules.Queries.Get;
public class GetTicketAlarmRulesQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetTicketAlarmRulesQuery, Result<IEnumerable<TicketAlarmRuleDto>>>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<IEnumerable<TicketAlarmRuleDto>>> Handle(GetTicketAlarmRulesQuery request, 
        CancellationToken cancellationToken)
    {
        return await _context.Roles
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<TicketAlarmRuleDto>(_mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
