using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Service;

using VogueUkraine.Profile.Worker.Repositories.Queue;

namespace VogueUkraine.Profile.Worker.Queue;

public class FinishContestTaskQueueService : QueueService<FinishContestTask>
{
    private readonly FinishContestTaskQueueProcessor _processor;

    public FinishContestTaskQueueService(FinishContestTaskQueueRepository queue,
        FinishContestTaskQueueProcessor processor) : base(queue)
    {
        _processor = processor;
    }

    protected override Task ProcessElementAsync(FinishContestTask element, CancellationToken stoppingToken = default)
    {
        return _processor.ProcessAsync(element.Id, stoppingToken);
    }
}