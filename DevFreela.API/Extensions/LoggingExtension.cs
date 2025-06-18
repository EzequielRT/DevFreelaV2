using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace DevFreela.API.Extensions;

public static class LoggingExtension
{
    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            var env = context.HostingEnvironment;
            var config = context.Configuration;
            var connString = config.GetConnectionString("SerilogConnection");

            configuration
                .ReadFrom.Configuration(config)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName();

            if (env.IsDevelopment())
            {
                configuration.MinimumLevel.Information()
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
            }
            else
            {
                configuration.MinimumLevel.Error()
                    .WriteTo.MSSqlServer(
                        connectionString: connString,
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "LogsApiSedam",
                            AutoCreateSqlTable = true
                        },
                        restrictedToMinimumLevel: LogEventLevel.Error,
                        columnOptions: GetSqlColumnOptions()
                    );
            }
        });        
    }

    private static ColumnOptions GetSqlColumnOptions()
    {
        var options = new ColumnOptions();
        options.Store.Clear();
        options.Store.Add(StandardColumn.TimeStamp);
        options.Store.Add(StandardColumn.Level);
        options.Store.Add(StandardColumn.Message);
        options.Store.Add(StandardColumn.Exception);
        options.Store.Add(StandardColumn.LogEvent); // evento como JSON
        return options;
    }
}
