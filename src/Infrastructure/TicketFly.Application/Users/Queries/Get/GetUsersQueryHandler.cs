namespace TicketFly.Application.Users.Queries.Get;
public class GetUsersQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetUsersQuery, Result<IEnumerable<UserDto>>>
{
    public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersQuery request, 
        CancellationToken cancellationToken)
    {
        return await context.Users
             .OrderByDescending(t => t.Created)
             .AsNoTracking()
             .ProjectTo<UserDto>(mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
