namespace VogueUkraine.Framework.Data.MongoDb.DbContext;

public class MongoDbContextOptions
{
    public string ConnectionString { get; set; }

    public string Database { get; set; }
}

public class MongoDbContextOptions<TContext> : MongoDbContextOptions where TContext : MongoDbContext
{
}