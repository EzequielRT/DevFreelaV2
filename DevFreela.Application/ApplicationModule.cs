using DevFreela.Application.Commands.Projects.Create;
using DevFreela.Application.Consumers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddHandlers()
            .AddValidators()
            .AddBackgroundServices();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationModule).Assembly));

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services) 
    {
        services.AddScoped<IValidator<CreateCommand>, CreateValidator>();

        return services;
    }

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services) 
    {
        services.AddHostedService<PaymentApprovedConsumer>();

        return services;
    }
}
