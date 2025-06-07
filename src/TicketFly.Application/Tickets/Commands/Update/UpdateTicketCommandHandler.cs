namespace TicketFly.Application.Tickets.Commands.Update;
public class UpdateTicketCommandHandler(ILogger logger, IAppDbContext context) : 
    IRequestHandler<UpdateTicketCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateTicketCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = context.Tickets.
            FirstOrDefault(x => x.Id == request.Id);

        if (entity is null)
        {
            return Result.Failure<bool>(
                Error.NotFound("Ticket not found", $"Ticket with ID {request.Id} not found."));
        }

        entity.Sender = request.Sender;

        context.Tickets.Update(entity);
        await context.SaveChangesAsync(cancellationToken);

        logger.Information("Ticket updated with ID: {Id}", entity.Id);

        return true;
    }
}