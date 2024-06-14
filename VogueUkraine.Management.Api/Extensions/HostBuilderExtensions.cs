using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace VogueUkraine.Management.Api.Extensions;

public static class HostBuilderExtensions
{
    public static WebApplicationBuilder SetupConfiguration(this WebApplicationBuilder builder)
    {
        var environmentName = builder.Environment.EnvironmentName;

        builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"secrets.{environmentName}.json", optional: true, reloadOnChange: true);

        builder.Configuration.AddEnvironmentVariables();

        return builder;
    }
    
    public static IWebHostBuilder SetUpKestrel(this IWebHostBuilder builder)
    {
        builder.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(
                int.TryParse(Environment.GetEnvironmentVariable("HTTP_PORT") ?? "5203", out var httpPort)
                    ? httpPort
                    : 5203,
                opt => opt.Protocols = HttpProtocols.Http1);
        });

        return builder;
    }
}