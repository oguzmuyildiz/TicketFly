using TicketFly.Application.UserRoles.Commands.Create;
using TicketFly.Domain.Constants;

namespace TicketFly.WebApi.Endpoints.UserRoles;

public record CreateUserRoleRequest(Guid UserId, Guid RoleId);

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("userroles", CreateUserRole)
            .WithTags(EndpointTags.UserRoles)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> CreateUserRole(CreateUserRoleRequest request, ISender sender, CancellationToken cancellationToken)
    {
        CreateUserRoleCommand command = new CreateUserRoleCommand(request.UserId, request.RoleId);
        Result<Guid> result = await sender.Send(command, cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}