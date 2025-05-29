namespace TicketFly.Application.Clients.Commands.Create;
public class CreateTicketCommandHandler : IRequestHandler<CreateClientCommand, Guid>
{
    public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        // Here you would typically interact with your database context to create a new client.
        return Guid.NewGuid();
    }
}
