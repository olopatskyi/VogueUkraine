using System;
using System.Threading;
using System.Threading.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage.Entities;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;
using Microsoft.Extensions.Logging;

namespace VogueUkraine.Framework.Services.QueueService.Processor;

public abstract class QueueElementProcessor<T, TIdentifier> : IQueueElementProcessor<TIdentifier> where T : QueueElementEntity<TIdentifier>
{
    private readonly IQueueRepository<T, TIdentifier> _queue;
    private readonly ILogger<QueueElementProcessor<T, TIdentifier>> _logger;
    protected QueueElementProcessor(IQueueRepository<T, TIdentifier> queue)
    {
        _queue = queue;
    }
    
    protected QueueElementProcessor(IQueueRepository<T, TIdentifier> queue,
        ILogger<QueueElementProcessor<T, TIdentifier>> logger)
    {
        _queue = queue;
        _logger = logger;
    }
        
    public async Task ProcessAsync(TIdentifier id, CancellationToken stoppingToken = default)
    {
        // try lock element
        var element = await _queue.BlockAndGetOneAsync(id, stoppingToken);
        // return if null (due to already blocker with other thread)
        if(element == null) return;
        // execute element task
        var complete = await ExecuteAsync(element, stoppingToken);
        // analyze response
        if (!complete)
            // -> delay job
            await _queue.DelayForAsync(element.Id, GetDelayForJob(element), stoppingToken);
        else
            // remove task from que
            await _queue.MarkAsProcessedAsync(element.Id, stoppingToken);
    }

    protected abstract Task<bool> ExecuteAsync(T element, CancellationToken stoppingToken = default);
        
    protected virtual TimeSpan GetDelayForJob(T element)
    {
        return element.DelayingCount switch
        {
            < 4 => TimeSpan.FromSeconds(2),
            < 10 => TimeSpan.FromMinutes(5),
            < 35 => TimeSpan.FromHours(1),
            _ => TimeSpan.FromDays(1)
        };
    }
}

public abstract class QueueElementProcessor<T> : QueueElementProcessor<T, string>, IQueueElementProcessor
    where T : QueueElementEntity<string>
{
    protected QueueElementProcessor(IQueueRepository<T> queue) : base(queue)
    {
    }
    
    protected QueueElementProcessor(IQueueRepository<T> queue, ILogger<QueueElementProcessor<T>> logger) : base(queue, logger)
    {
    }
}