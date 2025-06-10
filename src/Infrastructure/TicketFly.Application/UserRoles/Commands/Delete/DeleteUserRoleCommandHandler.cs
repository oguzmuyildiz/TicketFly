namespace TicketFly.Application.UserRoles.Commands.Delete;
public class DeleteUserRoleCommandHandler(IAppDbContext context) : 
    IRequestHandler<DeleteUserRoleCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteUserRoleCommand request, 
        CancellationToken cancellationToken)
    {
        var userRoles = await context.UserRoles
               .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);
        if (userRoles is null)
        {
            return Result.Failure<bool>(
                Error.NotFound("User Roles not found", $"User Roles with ID {request.Id} not found."));
        }
        context.UserRoles.Remove(userRoles);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}