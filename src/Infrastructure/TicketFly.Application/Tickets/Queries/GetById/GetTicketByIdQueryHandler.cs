namespace TicketFly.Application.Tickets.Queries.GetById;
public class GetTicketByIdQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetTicketByIdQuery, Result<TicketDto>>
{
    public async Task<Result<TicketDto>> Handle(GetTicketByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await context.Tickets
               .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<TicketDto>(Error.NotFound("Ticket not found", $"Ticket with ID {request.Id} not found."));
        }
        return mapper.Map<TicketDto>(result);
    }
}