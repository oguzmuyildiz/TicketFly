namespace TicketFly.Application.Tickets.Queries.GetByUserId;
public class GetTicketByUserIdQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetTicketByUserIdQuery, Result<TicketDto>>
{
    public async Task<Result<TicketDto>> Handle(GetTicketByUserIdQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await context.Tickets
               .FirstOrDefaultAsync(t => t.Id == request.UserId, cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<TicketDto>(
                Error.NotFound("Ticket not found", $"Ticket with UserID {request.UserId} not found."));
        }
        return mapper.Map<TicketDto>(result);
    }
}