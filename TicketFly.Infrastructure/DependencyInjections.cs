using TicketFly.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TicketFly.Application.Common.Intefaces.Data;
using TicketFly.Application.Common.Intefaces.Authentication;
using TicketFly.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TicketFly.Infrastructure.Data.Interceptors;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TicketFly.Infrastructure;
public static class DependencyInjections
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddDatabaseServices(services, configuration);
        AddAuthenticationServices(services, configuration);

        services.AddSingleton(TimeProvider.System);
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
    }

    private static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("TicketFlyMssqlConnection"));
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
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
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });

        services.AddAuthorization();

        return services;
    }
}
