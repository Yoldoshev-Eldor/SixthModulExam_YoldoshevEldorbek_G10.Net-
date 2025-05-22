using Serilog;

namespace UserContacts.Server.Configurations;

public static class SelilogConfiguration
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(dispose: true);
    }
}
