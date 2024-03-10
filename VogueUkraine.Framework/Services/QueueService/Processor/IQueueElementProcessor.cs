using System.Threading;
using System.Threading.Tasks;

namespace VogueUkraine.Framework.Services.QueueService.Processor;

public interface IQueueElementProcessor<in TIdentifier>
{
    Task ProcessAsync(TIdentifier id, CancellationToken stoppingToken = default);
}

public interface IQueueElementProcessor : IQueueElementProcessor<string>
{
    
}