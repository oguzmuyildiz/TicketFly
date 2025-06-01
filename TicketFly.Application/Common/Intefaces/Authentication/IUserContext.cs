namespace TicketFly.Application.Common.Intefaces.Authentication;

public interface IUserContext
{
    string? Id { get; }
    string? IpAddress { get; }
}
