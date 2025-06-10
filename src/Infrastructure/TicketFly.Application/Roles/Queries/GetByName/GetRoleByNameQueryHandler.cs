namespace TicketFly.Application.Roles.Queries.GetByName;
public class GetRoleByNameQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetRoleByNameQuery, Result<RoleDto>>
{
    public async Task<Result<RoleDto>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Roles
               .FirstOrDefaultAsync(t => t.Name == request.Name, cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<RoleDto>(Error.NotFound("Role not found", $"Role with Name {request.Name} not found."));
        }
        return mapper.Map<RoleDto>(result);
    }
}