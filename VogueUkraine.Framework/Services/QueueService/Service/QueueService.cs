using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage.Entities;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;
using MongoDB.Driver;

namespace VogueUkraine.Framework.Services.QueueService.Service;

public abstract class QueueService<T, TIdentifier, TBase> : IQueueService<TBase, TIdentifier> 
    where TBase : QueueElementEntity<TIdentifier>
    where T: TBase
{
    protected  IQueueRepository<T, TIdentifier> Queue { get; }

    protected QueueService(IQueueRepository<T, TIdentifier> queue)
    {
        Queue = queue;
    }
    
    public abstract Task AddAsync(TBase element, CancellationToken stoppingToken = default);

    public virtual async Task ProcessQueueAsync(CancellationToken stoppingToken = default)
    {
        var queueCursor = await Queue.GetQueueAsync(stoppingToken: stoppingToken);
        await ProcessJobsAsync(queueCursor, stoppingToken);
        
    }

    protected virtual async Task ProcessJobsAsync(IAsyncCursor<T> cursor,
        CancellationToken stoppingToken = default)
    {
        do
        {
            var hasNext = await cursor.MoveNextAsync(stoppingToken);
            if(!hasNext)
                break;
            
            await Task.WhenAll(cursor.Current.Select(job => ProcessElementAsync(job, stoppingToken)));
        } while (true);
    }

    protected abstract Task ProcessElementAsync(T element, CancellationToken stoppingToken = default);
}

public abstract class QueueService<T, TIdentifier> : QueueService<T, TIdentifier, T> where T : QueueElementEntity<TIdentifier>
{
    protected QueueService(IQueueRepository<T, TIdentifier> queue) : base(queue)
    {
    }

    public override Task AddAsync(T element, CancellationToken stoppingToken = default)
        => Queue.CreateAsync(element, stoppingToken);
}

public abstract class QueueService<T> : QueueService<T, string>, IQueueService<T> where T : QueueElementEntity<string>
{

    protected QueueService(IQueueRepository<T, string> queue) : base(queue)
    {
    }

    public Task AddManyAsync(IEnumerable<T> elements, CancellationToken stoppingToken = default) =>
        Queue.CreateManyAsync(elements, stoppingToken);
}