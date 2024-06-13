using Amazon;
using Amazon.Internal;
using Amazon.Runtime;
using Amazon.S3;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Data.MongoDb.DbContext;
using VogueUkraine.Framework.Extensions.ServiceCollection;
using VogueUkraine.Profile.Worker.Options;
using VogueUkraine.Profile.Worker.Queue;
using VogueUkraine.Profile.Worker.Repositories;
using VogueUkraine.Profile.Worker.Repositories.Abstractions;
using VogueUkraine.Profile.Worker.Repositories.Queue;
using VogueUkraine.Profile.Worker.Services;
using VogueUkraine.Profile.Worker.Services.Abstractions;

namespace VogueUkraine.Profile.Worker.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGeneralDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var options = configuration.GetSection("generalDb").Get<MongoDbContextOptions<VogueUkraineContext>>();
        serviceCollection.AddSingleton(options);
        serviceCollection.AddSingleton<VogueUkraineContext>();

        return serviceCollection;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<CreateContestTaskQueueRepository>();
        serviceCollection.AddSingleton<FinishContestTaskQueueRepository>();
        
        serviceCollection.AddSingleton<IParticipantRepository, ParticipantRepository>();
        serviceCollection.AddSingleton<IContestRepository, ContestRepository>();
        serviceCollection.AddSingleton<IVoteRepository, VoteRepository>();
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IFinishContestService, FinishContestService>();
        return serviceCollection;
    }

    public static IServiceCollection AddMongoQueues(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        //processor
        serviceCollection.AddSingleton<ParticipantImagesUploadTaskProcessor>();
        serviceCollection.AddSingleton<DeleteS3FilesTaskProcessor>();
        serviceCollection.AddSingleton<CreateContestTaskQueueProcessor>();
        serviceCollection.AddSingleton<FinishContestTaskQueueProcessor>();

        //queue
        serviceCollection.AddQueueService<ParticipantUploadImagesTask, ParticipantImagesUploadTaskQueueRepository,
            ContestantUploadImagesTaskQueueService>(configuration, "queues:participant_images_upload_queue");

        serviceCollection.AddQueueService<CreateContestTask, CreateContestTaskQueueRepository,
            CreateContestTaskQueueService>(configuration, "queues:create_contest_tasks_queue");

        serviceCollection.AddQueueService<FinishContestTask, FinishContestTaskQueueRepository,
            FinishContestTaskQueueService>(configuration, "queues:finish_contest_tasks_queue");

        serviceCollection.AddQueueService<DeleteS3FileTask, DeleteS3FilesTaskRepository, DeleteS3FilesTaskQueueService>(
            configuration, "queues:delete_s3_files_tasks");

        return serviceCollection;
    }

    public static IServiceCollection AddAwsS3Bucket(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var s3Options = configuration.GetSection("aws:s3").Get<AwsS3Options>();
        serviceCollection.AddSingleton(s3Options);

        serviceCollection.AddSingleton<IAmazonS3>(x =>
        {
            var awsCredentials = new BasicAWSCredentials(s3Options.AccessKey, s3Options.SecretKey);
            var s3Config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(s3Options.Region)
            };

            return new AmazonS3Client(awsCredentials, s3Config);
        });

        serviceCollection.AddSingleton<IS3Service, S3Service>();

        return serviceCollection;
    }
}