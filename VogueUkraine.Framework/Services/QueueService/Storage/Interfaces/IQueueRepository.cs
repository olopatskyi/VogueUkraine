using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage.Entities;
using MongoDB.Driver;

namespace VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;

public interface IQueueRepository<T, in TIdentifier> where T : QueueElementEntity<TIdentifier>
{
    Task<IAsyncCursor<T>> GetQueueAsync(int? batchSize = 100, CancellationToken stoppingToken = default);
    Task CreateAsync(T element, CancellationToken stoppingToken = default);
    Task CreateManyAsync(IEnumerable<T> elements, CancellationToken stoppingToken = default);
    Task<T> BlockAndGetOneAsync(TIdentifier id, CancellationToken stoppingToken = default);
    Task DelayForAsync(TIdentifier id, TimeSpan delayFor, CancellationToken stoppingToken = default);
    Task MarkAsProcessedAsync(TIdentifier id, CancellationToken stoppingToken = default);
}

public interface IQueueRepository<T> : IQueueRepository<T, string> where T : QueueElementEntity<string>
{
    
}