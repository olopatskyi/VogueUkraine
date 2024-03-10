using System.Threading.Tasks;
using MongoDB.Driver;

namespace VogueUkraine.Framework.Data.MongoDb.DbContext;

public interface IMongoDbContext
{
    IMongoDatabase Database { get; }

    Task MigrateAsync();
}