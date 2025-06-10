namespace TicketFly.Application.Roles.Commands.Create;
public class CreateRoleCommandHandler(IAppDbContext context) : IRequestHandler<CreateRoleCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Role
        {
            Name = request.Name
        };

        context.Roles.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
