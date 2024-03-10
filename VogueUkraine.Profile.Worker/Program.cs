using VogueUkraine.Profile.Worker;
using VogueUkraine.Profile.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args)
    .SetupConfiguration();

builder.Services.AddHostedService<Worker>();

builder.Services
    .AddGeneralDatabase(builder.Configuration)
    .AddRepositories()
    .AddMongoQueues(builder.Configuration)
    .AddAwsS3Bucket(builder.Configuration);
   
var host = builder.Build();
host.Run();