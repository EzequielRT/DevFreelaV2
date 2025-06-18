using Microsoft.OpenApi.Models;

namespace DevFreela.API.Extensions;

public static class SwaggerJwtExtension
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API - Sedam",
                Version = "v1"
            });            
        });

        return services;
    }

    public static WebApplication UseSwaggerWithDocs(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        });

        return app;
    }
}

