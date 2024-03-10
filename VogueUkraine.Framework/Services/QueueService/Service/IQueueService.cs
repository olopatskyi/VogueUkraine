using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage.Entities;

namespace VogueUkraine.Framework.Services.QueueService.Service;

public interface IQueueService<in T, TIdentifier> 
    where T : QueueElementEntity<TIdentifier>
{
    Task AddAsync(T element, CancellationToken stoppingToken = default);
    Task ProcessQueueAsync(CancellationToken stoppingToken = default);
}

public interface IQueueService<in T> : IQueueService<T, string> 
    where T : QueueElementEntity<string>
{
    Task AddManyAsync(IEnumerable<T> elements, CancellationToken stoppingToken = default);
}