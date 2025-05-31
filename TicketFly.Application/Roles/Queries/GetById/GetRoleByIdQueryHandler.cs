using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Entities;

namespace TicketFly.Application.Roles.Queries.GetById;

public class GetRoleByIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetRoleByIdQuery, Result<Role>>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Roles
               .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<Role>(Error.NotFound("Role not found", $"Role with ID {request.Id} not found."));
        }
        return _mapper.Map<Role>(result);
    }
}