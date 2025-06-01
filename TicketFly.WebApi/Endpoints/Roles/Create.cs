using TicketFly.Application.Roles.Commands.Create;
using TicketFly.Domain.Constants;
namespace TicketFly.WebApi.Endpoints.Roles;

public record CreateRoleRequest(string Name);

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("roles", CreateRole)
            .WithTags(EndpointTags.Roles)
            .RequireAuthorization([Policies.AdminPolicy]);
    }

    public static async Task<IResult> CreateRole(CreateRoleRequest request, ISender sender, CancellationToken cancellationToken)
    {
        CreateRoleCommand command = new CreateRoleCommand(request.Name);        
        Result<Guid> result = await sender.Send(command, cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
