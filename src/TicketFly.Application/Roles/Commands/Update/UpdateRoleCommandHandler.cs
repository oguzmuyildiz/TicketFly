namespace TicketFly.Application.Roles.Commands.Update;
public class UpdateRoleCommandHandler(IAppDbContext context) : IRequestHandler<UpdateRoleCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = context.Roles.FirstOrDefault(x => x.Id == request.Id);

        if (entity is null)
        {
            return Result.Failure<bool>(Error.NotFound("Role not found", $"Role with ID {request.Id} not found."));
        }

        entity.Name = request.Name;

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
