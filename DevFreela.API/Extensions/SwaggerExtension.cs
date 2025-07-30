using Microsoft.AspNetCore.Authentication.JwtBearer;
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
                Title = "API - DevFreela",
                Version = "v1"
            });   
            
            var jwtScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Coloque o token JWT no campo abaixo:\n\n**Bearer _seu_token_aqui_**",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition(jwtScheme.Reference.Id, jwtScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtScheme, Array.Empty<string>() }
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

