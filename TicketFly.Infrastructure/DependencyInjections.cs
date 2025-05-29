using Microsoft.Extensions.Hosting;
using TicketFly.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Infrastructure.Authentication;

namespace TicketFly.Infrastructure;
public static class DependencyInjections
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("TicketFlyMssqlConnection"));
        });
        builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
        builder.Services.AddSingleton<ITokenProvider, TokenProvider>();
    }
}
