using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace VogueUkraine.Identity.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplicationBuilder SetUpConfigurations(this WebApplicationBuilder builder,
        IWebHostEnvironment hostEnvironment)
    {
        var environment = hostEnvironment.EnvironmentName;

        builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"secrets.{environment}.json", optional: true, reloadOnChange: true);

        builder.Configuration.AddEnvironmentVariables();

        return builder;
    }

    public static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
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