using Amazon;
using Amazon.Internal;
using Amazon.Runtime;
using Amazon.S3;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Data.MongoDb.DbContext;
using VogueUkraine.Framework.Extensions.ServiceCollection;
using VogueUkraine.Profile.Worker.Options;
using VogueUkraine.Profile.Worker.Repositories;
using VogueUkraine.Profile.Worker.Repositories.Abstractions;
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
        serviceCollection.AddSingleton<IContestantRepository, ContestantRepository>();
        return serviceCollection;
    }

    public static IServiceCollection AddMongoQueues(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        //processor
        serviceCollection.AddSingleton<ContestantUploadImagesTaskProcessor>();
        serviceCollection.AddSingleton<DeleteS3FilesTaskProcessor>();

        //queue
        serviceCollection.AddQueueService<ContestantUploadImagesTask, ContestantUploadImagesTaskQueueRepository,
            ContestantUploadImagesTaskQueueService>(configuration, "queues:contestant_upload_images_queue");

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