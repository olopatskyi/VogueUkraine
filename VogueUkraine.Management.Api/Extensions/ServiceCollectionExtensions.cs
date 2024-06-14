using VogueUkraine.Data.Context;
using VogueUkraine.Framework.Data.MongoDb.DbContext;
using VogueUkraine.Management.Api.Managers;
using VogueUkraine.Management.Api.Managers.Abstractions;
using VogueUkraine.Management.Api.Repositories;
using VogueUkraine.Management.Api.Repositories.Abstractions;
using VogueUkraine.Management.Api.Repositories.Queue;
using VogueUkraine.Management.Api.Services;
using VogueUkraine.Management.Api.Services.Abstractions;

namespace VogueUkraine.Management.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddManagers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IParticipantManager, ParticipantManager>();
        serviceCollection.AddTransient<IContestManager, ContestManager>();
        serviceCollection.AddTransient<IVoteManager, VoteManager>();
        
        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IParticipantService, ParticipantService>();
        serviceCollection.AddTransient<IContestService, ContestService>();
        serviceCollection.AddTransient<IVoteService, VoteService>();
        
        return serviceCollection;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IParticipantRepository, ParticipantRepository>();
        serviceCollection.AddScoped<IContestRepository, ContestRepository>();
        serviceCollection.AddScoped<IVoteRepository, VoteRepository>();

        serviceCollection.AddScoped<IContestantUploadImagesTaskRepository, ContestantUploadImagesTaskRepository>();
        serviceCollection.AddScoped<IDeleteS3FilesTaskRepository, DeleteS3FilesTaskRepository>();
        serviceCollection.AddScoped<FinishContestTaskQueueRepository>();
        
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