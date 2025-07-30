using DevFreela.Core.Repositories;
using DevFreela.Infra.Auth;
using DevFreela.Infra.Persistence;
using DevFreela.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DevFreela.Infra;

public static class InfraModule
{
    public static IServiceCollection AddInfraModule(this IServiceCollection services, IConfiguration configuration)
    {
        services       
            .AddData(configuration)
            .AddAuth(configuration)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        //builder.Services.AddDbContext<DevFreelaDbContext>(options =>
        //  options.UseInMemoryDatabase("DevFreelaDb"));

        services.AddDbContext<DevFreelaDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProjectRepository, ProjectRepository>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!))
                };
            });

        return services;
    }
}
