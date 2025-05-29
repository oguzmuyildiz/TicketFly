using TicketFly.Application.Common.Interfaces;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Clients.Commands.Create;
public class CreateTicketCommandHandler(IAppDbContext context) : IRequestHandler<CreateClientCommand, Guid>
{
    private readonly IAppDbContext _context = context;
    public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = new Client
        {
            Name = request.Name,
            Email = request.Email,
            Domain = request.Domain
        };

        _context.Clients.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
