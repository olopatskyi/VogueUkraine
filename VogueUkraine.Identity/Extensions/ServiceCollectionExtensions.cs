using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using VogueUkraine.Framework.Data.MongoDb.DbContext;
using VogueUkraine.Identity.Data.Context;
using VogueUkraine.Identity.Managers;
using VogueUkraine.Identity.Managers.Abstractions;
using VogueUkraine.Identity.Options;
using VogueUkraine.Identity.Repository;
using VogueUkraine.Identity.Repository.Abstractions;
using VogueUkraine.Identity.Services;
using VogueUkraine.Identity.Services.Abstractions;

namespace VogueUkraine.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigurations(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var options = configuration.GetSection("identityDb").Get<MongoDbContextOptions<IdentityContext>>();
        serviceCollection.AddSingleton(options);

        var jwtOptions = configuration.GetSection("jwtOptions").Get<JwtOptions>();
        serviceCollection.AddSingleton(jwtOptions);

        return serviceCollection;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IdentityContext>();
        return serviceCollection;
    }

    public static IServiceCollection AddManagers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IIdentityManager, IdentityManager>();
        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IIdentityService, IdentityService>();
        return serviceCollection;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IAppUserRepository, AppUserRepository>();
        serviceCollection.AddSingleton<IAppUserTokensRepository, AppUserTokensRepository>();

        return serviceCollection;
    }

    public static IServiceCollection SetUpAuthentication(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtOptions:Key"));
        serviceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration.GetValue<string>("JwtOptions:Issuer"),
                    ValidAudience = configuration.GetValue<string>("JwtOptions:Audience"),
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return serviceCollection;
    }
}