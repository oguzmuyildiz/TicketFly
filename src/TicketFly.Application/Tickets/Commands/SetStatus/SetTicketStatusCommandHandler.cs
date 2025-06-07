namespace TicketFly.Application.Tickets.Commands.SetStatus;
public class SetTicketStatusCommandHandler(IAppDbContext context) : 
    IRequestHandler<SetTicketStatusCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SetTicketStatusCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = context.Tickets.FirstOrDefault(x => x.Id == request.Id);

        if (entity is null)
        {
            return Result.Failure<bool>(
                Error.NotFound("Ticket not found", $"Ticket with ID {request.Id} not found."));
        }

        entity.Status = request.TicketStatus;

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
