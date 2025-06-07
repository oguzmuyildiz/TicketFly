namespace TicketFly.Application.Tickets.Commands.Create;
public class CreateTicketCommandHandler(ILogger logger, IAppDbContext context) : 
    IRequestHandler<CreateTicketCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateTicketCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = new Ticket
        {
            Content = request.Content,
            Title = request.Title,
            Sender = request.Sender,
            Domain = request.Domain,
            Status = request.Status,
            AssignedToId = request.AssignedToId,
            ClientId = request.ClientId
        };

        context.Tickets.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        logger.Information("Ticket created with ID: {Id}", entity.Id);

        return entity.Id;
    }
}