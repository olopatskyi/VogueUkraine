using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage.Entities;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace VogueUkraine.Framework.Services.QueueService.Storage;

public abstract class QueueRepository<T, TIdentifier> : IQueueRepository<T, TIdentifier> where T : QueueElementEntity<TIdentifier>
{
    protected readonly IMongoCollection<T> Collection;

    protected QueueRepository(IMongoCollection<T> collection)
    {
        Collection = collection;
    }
        
    public virtual async Task<IAsyncCursor<T>> GetQueueAsync(int? batchSize = 100,
        CancellationToken stoppingToken = default)
        => await Collection.FindAsync(j=>
                !j.DelayedTill.HasValue ||
                j.DelayedTill.Value < DateTime.Now
            , new FindOptions<T>{NoCursorTimeout = true, BatchSize = batchSize}, 
            stoppingToken);

    public virtual async Task CreateAsync(T element, CancellationToken stoppingToken = default)
        => await Collection.InsertOneAsync(element, null, stoppingToken);

    public async Task CreateManyAsync(IEnumerable<T> elements, CancellationToken stoppingToken = default)
        => await Collection.InsertManyAsync(elements, cancellationToken: stoppingToken);

    public virtual async Task<T> BlockAndGetOneAsync(TIdentifier id, CancellationToken stoppingToken = default)
        => await Collection.FindOneAndUpdateAsync(
            Builders<T>.Filter.And(
                Builders<T>.Filter.Eq(x => x.Id, id),
                Builders<T>.Filter.Or(
                    Builders<T>.Filter.Exists(x => x.DelayedTill, false),
                    Builders<T>.Filter.Eq(x => x.DelayedTill, (DateTime?) BsonNull.Value),
                    Builders<T>.Filter.Lt(x => x.DelayedTill, DateTime.Now)
                )
            ),
            GetDelayUpdate(TimeSpan.FromMinutes(5)),
            new FindOneAndUpdateOptions<T> { ReturnDocument = ReturnDocument.After },
            stoppingToken
        );

    public virtual async Task DelayForAsync(TIdentifier id, TimeSpan delayFor, CancellationToken stoppingToken = default)
        => await Collection.FindOneAndUpdateAsync(
            Builders<T>.Filter.Eq(x=>x.Id, id),
            GetDelayUpdate(delayFor)
                .Inc(d=>d.DelayingCount, 1), 
            null,
            stoppingToken
        );

    public virtual async Task MarkAsProcessedAsync(TIdentifier id, CancellationToken stoppingToken = default)
        => await Collection.FindOneAndDeleteAsync(
            Builders<T>.Filter.Eq(x=>x.Id, id),
            null,
            stoppingToken);
        
    private static UpdateDefinition<T> GetDelayUpdate(TimeSpan delayFor)
        => Builders<T>.Update.Set(d => d.DelayedTill,
            DateTime.Now.AddMilliseconds(delayFor.TotalMilliseconds));
}

public abstract class QueueRepository<T> : QueueRepository<T, string>, IQueueRepository<T> where T : QueueElementEntity<string>
{
    protected QueueRepository(IMongoCollection<T> collection) : base(collection)
    {
    }
}