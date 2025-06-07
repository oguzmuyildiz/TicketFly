namespace TicketFly.Application.EmailAccounts.Commands.Update;
public class UpdateEmailAccountCommandHandler(ILogger logger, IAppDbContext context) : IRequestHandler<UpdateEmailAccountCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateEmailAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = context.EmailAccounts.FirstOrDefault(x => x.Id == request.Id);

        if (entity is null)
        {
            return Result.Failure<bool>(Error.NotFound("EmailAccount not found", $"EmailAccount with ID {request.Id} not found."));
        }

        entity.Email = request.Email;

        context.EmailAccounts.Update(entity);
        await context.SaveChangesAsync(cancellationToken);

        logger.Information("EmailAccount updated with ID: {Id}", entity.Id);

        return true;
    }
}
