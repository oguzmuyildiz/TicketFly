namespace TicketFly.Application.Roles.Queries.GetById;
public class GetRoleByIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
{
    public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Roles
               .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<RoleDto>(Error.NotFound("Role not found", $"Role with ID {request.Id} not found."));
        }
        return mapper.Map<RoleDto>(result);
    }
}