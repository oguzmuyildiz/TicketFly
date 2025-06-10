namespace TicketFly.Application.Common.Intefaces.Authentication;

public interface IPasswordHasher
{
    string Hash(string password);

    bool Verify(string password, string passwordHash);
}
