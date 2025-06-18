namespace DevFreela.API.Extensions;

public static class ConfigurationBuilderExtension
{
    public static IConfigurationBuilder AddEnvironmentJsonFiles(this IConfigurationBuilder configurationBuilder, IWebHostEnvironment env)
    {
        return configurationBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    }
}
