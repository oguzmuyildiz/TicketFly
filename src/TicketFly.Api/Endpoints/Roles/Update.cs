using TicketFly.Application.Roles.Commands.Update;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Roles;

public record UpdateRoleRequest(Guid Id, string Name);

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("roles", UpdateRole)
            .WithTags(EndpointTags.Roles)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> UpdateRole(UpdateRoleRequest request, ISender sender, CancellationToken cancellationToken)
    {
        Result<bool> result = await sender.Send(new UpdateRoleCommand(request.Id, request.Name), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
