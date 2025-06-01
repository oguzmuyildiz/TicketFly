using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;
using TicketFly.Domain.Events;

namespace TicketFly.Application.Clients.Commands.Create;
public class CreateClientCommandHandler(IAppDbContext context) : IRequestHandler<CreateClientCommand, Result<Guid>>
{
    private readonly IAppDbContext _context = context;
    public async Task<Result<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = new Client
        {
            Name = request.Name,
            Email = request.Email,
            Domain = request.Domain
        };

        entity.AddDomainEvent(new ClientCreatedEvent(entity));

        _context.Clients.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
