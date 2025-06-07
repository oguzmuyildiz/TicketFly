using TicketFly.Application.Organizations.Commands.Create;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Organizations;

public record CreateOrganizationRequest(
    Guid Id,
    string Name,
    string Description,
    string Logo,
    string Website,
    string ContactEmail,
    string ContactPhone);

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("organization", CreateOrganization)
            .WithTags(EndpointTags.Roles)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> CreateOrganization(CreateOrganizationRequest request, 
        ISender sender, CancellationToken cancellationToken)
    {
        Result<Guid> result = await sender.Send(
            new CreateOrganizationCommand(request.Name, request.Description, request.Logo,
                request.Website, request.ContactEmail, request.ContactPhone), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
