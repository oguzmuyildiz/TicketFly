namespace TicketFly.Application.Organizations.Commands.Create;
public class CreateOrganizationCommandHandler(ILogger logger, IAppDbContext context) : IRequestHandler<CreateOrganizationCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var entity = new Organization
        {
            Name = request.Name,
            Description = request.Description,
            Logo = request.Logo,
            Website = request.Website,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone
        };

        context.Organizations.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        logger.Information("Organization created with ID: {Id}", entity.Id);

        return entity.Id;
    }
}