using Microsoft.Extensions.Hosting;
using TicketFly.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TicketFly.Infrastructure.Data.Interceptors;

namespace TicketFly.Infrastructure;
public static class DependencyInjections
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {

        builder.Services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("TicketFlyMssqlConnection"));
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

        builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
        builder.Services.AddSingleton<ITokenProvider, TokenProvider>();
    }
}
