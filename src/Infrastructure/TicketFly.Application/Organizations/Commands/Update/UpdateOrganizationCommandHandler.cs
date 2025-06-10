namespace TicketFly.Application.Organizations.Commands.Update;
public class UpdateOrganizationCommandHandler(ILogger<UpdateOrganizationCommandHandler> logger, IAppDbContext context) : IRequestHandler<UpdateOrganizationCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var entity = context.Organizations.FirstOrDefault(x => x.Id == request.Id);

        if (entity is null)
        {
            return Result.Failure<bool>(Error.NotFound("Organization not found", $"Organization with ID {request.Id} not found."));
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Logo = request.Logo;
        entity.Website = request.Website;
        entity.ContactEmail = request.ContactEmail;
        entity.ContactPhone = request.ContactPhone;

        context.Organizations.Update(entity);

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Organization updated with ID: {Id}", entity.Id);

        return true;
    }
}