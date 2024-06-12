using MongoDB.Driver;

namespace VogueUkraine.Framework.Data.Abstractions.MongoDb;

public abstract class BasicEntityConfiguration<T>
{
    private readonly IMongoCollection<T> _collection;

    protected BasicEntityConfiguration(IMongoCollection<T> collection)
    {
        _collection = collection;
    }

    public Task GetMigrationTask()
        => _collection.Indexes.CreateManyAsync(
            IndicesConfiguration()
        );

    protected virtual IEnumerable<CreateIndexModel<T>> IndicesConfiguration()
        => new List<CreateIndexModel<T>>();
}