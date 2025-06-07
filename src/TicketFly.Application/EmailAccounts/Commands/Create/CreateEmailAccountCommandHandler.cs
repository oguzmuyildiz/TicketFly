using TicketFly.Domain.Events;

namespace TicketFly.Application.EmailAccounts.Commands.Create;
public class CreateEmailAccountCommandHandler(IAppDbContext context) : IRequestHandler<CreateEmailAccountCommand, Result<Guid>>
{
    private readonly IAppDbContext _context = context;
    public async Task<Result<Guid>> Handle(CreateEmailAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = new EmailAccount
        {
            OrganizationId = request.OrganizationId,
            Email = request.Email,
            ApiKey = request.ApiKey,
            Host = request.Host,
            Port = request.Port,
            Provider = request.Provider,
            IsActive = request.IsActive
        };

        entity.AddDomainEvent(new EmailAccountCreatedEvent(entity));

        _context.EmailAccounts.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
