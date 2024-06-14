namespace VogueUkraine.Management.Worker.Extensions;

public static class HostBuilderExtensions
{
    public static HostApplicationBuilder SetupConfiguration(this HostApplicationBuilder builder)
    {
        var environmentName = builder.Environment.EnvironmentName;

        builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"secrets.{environmentName}.json", optional: true, reloadOnChange: true);

        builder.Configuration.AddEnvironmentVariables();

        return builder;
    }
}