namespace TicketFly.Application.Organizations.Queries.GetById;
public class GetOrganizationByIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetOrganizationByIdQuery, Result<OrganizationDto>>
{
    public async Task<Result<OrganizationDto>> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Organizations
               .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<OrganizationDto>(Error.NotFound("Organization not found", $"Organization with ID {request.Id} not found."));
        }
        return mapper.Map<OrganizationDto>(result);
    }
}