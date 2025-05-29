using TicketFly.Application.Common.Intefaces.Data;

namespace TicketFly.Application.Clients.Commands.Update;
public class UpdateClientCommandHandler(IAppDbContext context) : IRequestHandler<UpdateClientCommand, bool>
{
    public async Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = context.Clients.FirstOrDefault(x => x.Id == request.Id);
        if (entity == null)
            return false;

        entity.Email = request.Email;
        entity.Name = request.Name;
        entity.Domain = request.Domain;

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
