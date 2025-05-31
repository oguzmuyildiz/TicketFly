using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.UserRoles.Commands.Create;
public class CreateUserRoleCommandHandler(IAppDbContext context) : IRequestHandler<CreateUserRoleCommand, Result<Guid>>
{
    private readonly IAppDbContext _context = context;
    public async Task<Result<Guid>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId
        };

        _context.UserRoles.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
