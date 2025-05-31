using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Roles.Commands.Create;
public class CreateRoleCommandHandler(IAppDbContext context) : IRequestHandler<CreateRoleCommand, Result<Guid>>
{
    private readonly IAppDbContext _context = context;
    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Role
        {
            Name = request.Name
        };

        _context.Roles.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
