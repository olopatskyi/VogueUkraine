using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VogueUkraine.Framework.Extensions.DateTime;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace VogueUkraine.Framework.Services.QueueService.Storage.Entities;

[BsonIgnoreExtraElements]
// ReSharper disable once ClassNeverInstantiated.Global
public class QueueElementEntity<T>
{
    [BsonId]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public virtual T Id { get; set; }
    [BsonElement("dt"), BsonIgnoreIfNull]
    public DateTime? DelayedTill
    {
        get => _delayedTill;
        set => _delayedTill = value?.Truncate(TruncateTo.Seconds);
    }
    private DateTime? _delayedTill;
    [BsonElement("dc"), BsonIgnoreIfNull] 
    public int? DelayingCount { get; set; } = 0;
    [BsonElement("cat")]
    public DateTime? CreatedAt
    {
        get => _createdAt;
        set => _createdAt = value?.Truncate(TruncateTo.Seconds);
    }
    private DateTime? _createdAt = DateTime.Now.Truncate(TruncateTo.Seconds);
}

public class QueueElementEntityConfiguration<T, TIdentifier> where T : QueueElementEntity<TIdentifier>
{
    private readonly IMongoCollection<T> _collection;

    protected QueueElementEntityConfiguration(IMongoCollection<T> collection)
    {
        _collection = collection;
    }

    public Task GetMigrationTask()
        => _collection.Indexes.CreateManyAsync(
            IndicesConfiguration()
        );

    protected virtual IEnumerable<CreateIndexModel<T>> IndicesConfiguration()
        => new List<CreateIndexModel<T>>
        {
            new
            (
                Builders<T>.IndexKeys.Descending(x => x.DelayedTill),
                new CreateIndexOptions { Background = true })
        };
}

[BsonIgnoreExtraElements]
public class QueueElementEntity : QueueElementEntity<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();
}

public class QueueElementEntityConfiguration<T> : QueueElementEntityConfiguration<T, string> where T : QueueElementEntity<string>
{
    public QueueElementEntityConfiguration(IMongoCollection<T> collection) : base(collection)
    {
    }
}