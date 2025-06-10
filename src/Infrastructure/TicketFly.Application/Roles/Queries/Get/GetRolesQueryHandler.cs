namespace TicketFly.Application.Roles.Queries.Get;
public class GetRolesQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetRolesQuery, Result<IEnumerable<RoleDto>>>
{
    public async Task<Result<IEnumerable<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await context.Roles
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<RoleDto>(mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
