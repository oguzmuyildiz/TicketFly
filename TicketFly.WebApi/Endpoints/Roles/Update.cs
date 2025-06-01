using TicketFly.Application.Roles.Commands.Update;
using TicketFly.Domain.Constants;

namespace TicketFly.WebApi.Endpoints.Roles;

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
        UpdateRoleCommand command = new(request.Id, request.Name);
        Result<bool> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
