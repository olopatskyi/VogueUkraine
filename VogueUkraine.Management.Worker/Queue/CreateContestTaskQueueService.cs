using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Service;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;
using VogueUkraine.Management.Worker.Repositories.Queue;

namespace VogueUkraine.Management.Worker.Queue;

public class CreateContestTaskQueueService : QueueService<CreateContestTask>
{
    private readonly CreateContestTaskQueueProcessor _processor;

    public CreateContestTaskQueueService(CreateContestTaskQueueRepository queue,
        CreateContestTaskQueueProcessor processor) : base(queue)
    {
        _processor = processor;
    }

    protected override Task ProcessElementAsync(CreateContestTask element, CancellationToken stoppingToken = default)
    {
        return _processor.ProcessAsync(element.Id, stoppingToken);
    }
}