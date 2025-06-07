namespace TicketFly.Application.Organizations.Queries.Get;
public class GetOrganizationsQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetOrganizationsQuery, Result<IEnumerable<OrganizationDto>>>
{
    public async Task<Result<IEnumerable<OrganizationDto>>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        return await context.Organizations
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<OrganizationDto>(mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
