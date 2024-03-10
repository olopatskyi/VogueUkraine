using VogueUkraine.Data.Context;
using VogueUkraine.Framework.Data.MongoDb.DbContext;
using VogueUkraine.Profile.Api.Managers;
using VogueUkraine.Profile.Api.Managers.Abstractions;
using VogueUkraine.Profile.Api.Repositories;
using VogueUkraine.Profile.Api.Repositories.Abstractions;
using VogueUkraine.Profile.Api.Services;
using VogueUkraine.Profile.Api.Services.Abstractions;

namespace VogueUkraine.Profile.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddManagers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IContestantManager, ContestantManager>();
        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IContestantService, ContestantService>();
        return serviceCollection;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IContestantRepository, ContestantRepository>();
        serviceCollection.AddScoped<IContestantUploadImagesTaskRepository, ContestantUploadImagesTaskRepository>();
        serviceCollection.AddScoped<IDeleteS3FilesTaskRepository, DeleteS3FilesTaskRepository>();

        return serviceCollection;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var options = configuration.GetSection("generalDb").Get<MongoDbContextOptions<VogueUkraineContext>>();
        serviceCollection.AddSingleton(options);
        serviceCollection.AddSingleton<VogueUkraineContext>();

        return serviceCollection;
    }
}