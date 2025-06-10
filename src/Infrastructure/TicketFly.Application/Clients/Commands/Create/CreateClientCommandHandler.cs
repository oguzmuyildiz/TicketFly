using TicketFly.Domain.Events;

namespace TicketFly.Application.Clients.Commands.Create;
public class CreateClientCommandHandler(ILogger<CreateClientCommandHandler> logger, IAppDbContext context) : IRequestHandler<CreateClientCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = new Client
        {
            OrganizationId = request.OrganizationId,
            Name = request.Name,
            Email = request.Email,
            Domain = request.Domain
        };

        entity.AddDomainEvent(new ClientCreatedEvent(entity));

        context.Clients.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Client created: {ClientId} - {ClientEmail}", entity.Id, entity.Email);
        return entity.Id;
    }
}
