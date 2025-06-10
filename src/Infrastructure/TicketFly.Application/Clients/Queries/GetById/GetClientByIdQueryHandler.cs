namespace TicketFly.Application.Clients.Queries.GetById;
public class GetClientByIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetClientByIdQuery, Result<ClientDto>>
{
    public async Task<Result<ClientDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await context.Clients
               .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        if (client is null)
        {
            return Result.Failure<ClientDto>(Error.NotFound("Client not found", $"Client with ID {request.Id} not found."));
        }

        return mapper.Map<ClientDto>(client);
    }
}