namespace TicketFly.Domain.Constants;

public abstract class Policies
{
    public const string CanDelete = nameof(CanDelete);
    public const string AdminPolicy = nameof(AdminPolicy);
    public const string UserPolicy = nameof(UserPolicy);
}