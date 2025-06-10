namespace TicketFly.Application.Organizations.Commands.Create;
public class CreateOrganizationCommandHandler(IAppDbContext context) : IRequestHandler<CreateOrganizationCommand, Result<Guid>>
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

        return entity.Id;
    }
}