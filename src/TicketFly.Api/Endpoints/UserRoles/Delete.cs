using TicketFly.Application.UserRoles.Commands.Delete;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.UserRoles;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapDelete("userroles", DeleteUserRole)
            .WithTags(EndpointTags.UserRoles)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> DeleteUserRole(Guid Id,
        ISender sender, CancellationToken cancellationToken)
    {
        Result<bool> result = await sender.Send(
            new DeleteUserRoleCommand(Id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}