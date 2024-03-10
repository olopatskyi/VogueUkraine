using VogueUkraine.Framework.Services.QueueService.Storage.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VogueUkraine.Framework.Services.QueueService.Service;

public class QueueWorker<T, TIdentifier> : BackgroundService where T : QueueElementEntity<TIdentifier>
{
    private readonly IQueueService<T, TIdentifier> _service;
    private readonly ILogger<QueueWorker<T, TIdentifier>> _logger;

    public QueueWorker(IQueueService<T, TIdentifier> service, ILogger<QueueWorker<T, TIdentifier>> logger)
    {
        _service = service;
        _logger = logger;
    }
        
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _service.ProcessQueueAsync(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}

public class QueueWorker<T> : QueueWorker<T, string> where T : QueueElementEntity<string>
{
    public QueueWorker(IQueueService<T> service, ILogger<QueueWorker<T>> logger) : base(service, logger)
    {
    }
}