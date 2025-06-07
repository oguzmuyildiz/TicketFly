namespace TicketFly.Application.Organizations.Commands.Update;
public record UpdateOrganizationCommand(Guid Id, string Name, 
    string Description, string Logo, 
    string Website, string ContactEmail, 
    string ContactPhone) : IRequest<Result<bool>>;