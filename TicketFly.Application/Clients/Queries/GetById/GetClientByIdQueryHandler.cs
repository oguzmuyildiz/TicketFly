using Microsoft.EntityFrameworkCore;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Dtos;

namespace TicketFly.Application.Clients.Queries.GetById;

public class GetClientByIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetClientByIdQuery, ClientDto>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<ClientDto> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
               .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);
        return _mapper.Map<ClientDto>(client);
    }
}