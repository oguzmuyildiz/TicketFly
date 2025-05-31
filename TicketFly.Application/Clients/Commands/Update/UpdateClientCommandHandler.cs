using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Domain.Common;
using TicketFly.Domain.Dtos;
using TicketFly.Domain.Entities;
using TicketFly.Domain.Exceptions;

namespace TicketFly.Application.Clients.Commands.Update;
public class UpdateClientCommandHandler(IAppDbContext context) : IRequestHandler<UpdateClientCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = context.Clients.FirstOrDefault(x => x.Id == request.Id);
        if (entity is null)
            return Result.Failure<bool>(Error.NotFound("Client not found", $"Client with ID {request.Id} not found."));

        entity.Email = request.Email;
        entity.Name = request.Name;
        entity.Domain = request.Domain;

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
