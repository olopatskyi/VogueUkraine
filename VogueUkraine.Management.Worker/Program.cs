using System.Reflection;
using VogueUkraine.Management.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args)
    .SetupConfiguration();


builder.Services
    .AddGeneralDatabase(builder.Configuration)
    .AddRepositories()
    .AddServices()
    .AddMongoQueues(builder.Configuration)
    .AddAwsS3Bucket(builder.Configuration)
    .AddAutoMapper(Assembly.GetExecutingAssembly());
   
var host = builder.Build();
host.Run();