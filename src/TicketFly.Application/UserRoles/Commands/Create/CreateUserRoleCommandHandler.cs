namespace TicketFly.Application.UserRoles.Commands.Create;
public class CreateUserRoleCommandHandler(IAppDbContext context) : 
    IRequestHandler<CreateUserRoleCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserRoleCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId
        };

        context.UserRoles.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
