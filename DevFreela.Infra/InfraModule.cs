using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistence;
using DevFreela.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infra;

public static class InfraModule
{
    public static IServiceCollection AddInfraModule(this IServiceCollection services, IConfiguration configuration)
    {
        services       
            .AddData(configuration)
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
}
