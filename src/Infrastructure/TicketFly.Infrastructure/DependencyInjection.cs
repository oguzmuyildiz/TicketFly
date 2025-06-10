using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Domain.Constants;
using TicketFly.Infrastructure.Data;
using TicketFly.Infrastructure.Authentication;
using TicketFly.Infrastructure.Data.Interceptors;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TicketFly.Infrastructure;
public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDatabaseServices(builder.Configuration).
        AddAuthenticationServices(builder.Configuration).
        AddAuthorizationServices();

        builder.Services.AddScoped<AppDbInitialiser>();
        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
        builder.Services.AddSingleton<ITokenProvider, TokenProvider>();
        return builder;
    }

    private static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("TicketFlyMssqlConnection")!;
            var password = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
            var connStr = string.Format(connectionString, password);
            
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connStr).AddAsyncSeeding(sp);
        });
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        return services;
    }
    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = 
            options.DefaultChallengeScheme =
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });

        return services;
    }
    private static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(Policies.CanDelete, policy => policy.RequireRole(Roles.Administrator))
            .AddPolicy(Policies.AdminPolicy, policy => policy.RequireRole(Roles.Administrator))
            .AddPolicy(Policies.UserPolicy, policy => policy.RequireRole(Roles.User));
        return services;
    }
}
