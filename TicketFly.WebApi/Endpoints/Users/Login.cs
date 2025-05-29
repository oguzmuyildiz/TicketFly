using TicketFly.Application.Clients.Commands.Create;
using TicketFly.Application.Users.Commands.Login;
using TicketFly.WebApi.Endpoints.Clients;

namespace TicketFly.WebApi.Endpoints.Users;

public record LoginUserRequest(string Email, string Password);

public class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("users/login", LoginUser)
            .WithTags(EndpointTags.Clients);
    }

    public static async Task<IResult> LoginUser(LoginUserRequest request, ISender sender, CancellationToken cancellationToken)
    {
        LoginUserCommand command = new(request.Email, request.Password);
        string token = await sender.Send(command, cancellationToken);
        return Results.Ok(token);
    }
}
