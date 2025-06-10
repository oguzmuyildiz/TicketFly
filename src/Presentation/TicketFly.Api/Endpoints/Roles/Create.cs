using TicketFly.Application.Roles.Commands.Create;
using TicketFly.Domain.Constants;

namespace TicketFly.Api.Endpoints.Roles;

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
        Result<Guid> result = await sender.Send(new CreateRoleCommand(request.Name), cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
