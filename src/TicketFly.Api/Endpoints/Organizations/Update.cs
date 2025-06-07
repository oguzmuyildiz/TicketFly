using TicketFly.Application.Organizations.Commands.Update;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Organizations;

public record UpdateOrganizationRequest(
    Guid Id,
    string Name,
    string Description, 
    string Logo, 
    string Website,
    string ContactEmail,
    string ContactPhone);

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("organizations", UpdateOrganization)
            .WithTags(EndpointTags.Organizations)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> UpdateOrganization(UpdateOrganizationRequest request, 
        ISender sender, CancellationToken cancellationToken)
    {
        Result<bool> result = await sender.Send(
            new UpdateOrganizationCommand(request.Id, request.Name, request.Description, request.Logo, 
                request.Website, request.ContactEmail, request.ContactPhone), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
