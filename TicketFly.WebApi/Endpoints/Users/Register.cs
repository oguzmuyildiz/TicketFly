using TicketFly.Application.Users.Commands.Login;
using TicketFly.Application.Users.Commands.Register;

namespace TicketFly.WebApi.Endpoints.Users;

public record RegisterUserRequest(string Email, string UserName, string FirstName, string LastName, string Password);

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("users/register", RegisterUser)
            .WithTags(EndpointTags.Clients);
    }

    public static async Task<IResult> RegisterUser(RegisterUserRequest request, ISender sender, CancellationToken cancellationToken)
    {
        RegisterUserCommand command = new(request.Email, request.UserName, request.FirstName, request.LastName, request.Password);
        string id = await sender.Send(command, cancellationToken);
        return Results.Ok(id);
    }
}
