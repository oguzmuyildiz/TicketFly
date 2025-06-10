using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicketFly.Application.Roles.Commands.Create;
using TicketFly.Application.Roles.Queries.GetByName;
using TicketFly.Application.UserRoles.Commands.Create;
using TicketFly.Application.Users.Commands.Register;
using TicketFly.Application.Users.Queries.GetByEmail;
using TicketFly.Domain.Constants;

namespace TicketFly.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static void AddAsyncSeeding(this DbContextOptionsBuilder builder, IServiceProvider serviceProvider)
    {
        builder.UseAsyncSeeding(async (context, _, ct) =>
        {
            var initialiser = serviceProvider.GetRequiredService<AppDbInitialiser>();

            await initialiser.SeedAsync();
        });
    }

    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<AppDbInitialiser>();

        await initialiser.InitialiseAsync();
    }
}

public class AppDbInitialiser
{
    private readonly AppDbContext _context;
    private readonly ISender _sender;
    private readonly ILogger<AppDbInitialiser> _logger;
    public AppDbInitialiser(AppDbContext context, ILogger<AppDbInitialiser> logger, ISender sender)
    {
        _context = context;
        _logger = logger;
        _sender = sender;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }


    public async Task TrySeedAsync()
    {
        //Create default roles if they do not exist

        var adminRole = await _sender.Send(new GetRoleByNameQuery(Roles.Administrator));
        var adminRoleId = adminRole.IsSuccess ? adminRole.Value.Id : Guid.Empty;
        if(adminRoleId == Guid.Empty)
        {
            var createdRoleId = await _sender.Send(new CreateRoleCommand(Roles.Administrator));
            adminRoleId = createdRoleId.Value;
        }


        var userRole = await _sender.Send(new GetRoleByNameQuery(Roles.User));
        var userRoleId = userRole.IsSuccess ? userRole.Value.Id : Guid.Empty;
        if (userRoleId == Guid.Empty)
        {
            var createdRoleId = await _sender.Send(new CreateRoleCommand(Roles.User));
            userRoleId = createdRoleId.Value;
        }

        var adminUserEmail = "admin@localhost";
        var adminUserPassword = "Admin1!";
        var adminUser = await _sender.Send(new GetUserByEmailQuery(adminUserEmail));
        var adminUserId = adminUser.IsSuccess ? adminUser.Value.Id : Guid.Empty;
        if (adminRole.IsFailure)
        {
            var createdAdminId = await _sender.Send(new RegisterUserCommand(adminUserEmail, adminUserEmail, adminUserEmail, adminUserEmail, adminUserPassword));
            adminUser = await _sender.Send(new GetUserByEmailQuery(adminUserEmail));
            adminUserId = adminUser.IsSuccess ? adminUser.Value.Id : Guid.Empty;
        }

        var createdUserRoleId = await _sender.Send(new CreateUserRoleCommand(adminUserId, adminRoleId));

        await Task.Delay(1000); // Simulate some delay for seeding
        _logger.LogError("try seed data");
    }
}
