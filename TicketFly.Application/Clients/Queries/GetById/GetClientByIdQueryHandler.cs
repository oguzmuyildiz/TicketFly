using Microsoft.EntityFrameworkCore;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Common;
using TicketFly.Domain.Dtos;
using TicketFly.Domain.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TicketFly.Application.Clients.Queries.GetById;

public class GetClientByIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetClientByIdQuery, Result<ClientDto>>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<ClientDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
               .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        //if (client == null)
        //    throw new ProblemException("Client not found", $"Client with ID {request.Id} not found.");

        if (client is null)
        {
            return Result.Failure<ClientDto>(Error.NotFound("Client not found", $"Client with ID {request.Id} not found."));
        }
        return _mapper.Map<ClientDto>(client);
    }
}