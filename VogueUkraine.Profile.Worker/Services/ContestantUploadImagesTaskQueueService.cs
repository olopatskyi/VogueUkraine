using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Service;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;

namespace VogueUkraine.Profile.Worker.Services;

public class ContestantUploadImagesTaskQueueService : QueueService<ContestantUploadImagesTask>
{
    private readonly ContestantUploadImagesTaskProcessor _processor;

    public ContestantUploadImagesTaskQueueService(IQueueRepository<ContestantUploadImagesTask> queue,
        ContestantUploadImagesTaskProcessor processor) : base(queue)
    {
        _processor = processor;
    }

    protected override Task ProcessElementAsync(ContestantUploadImagesTask element,
        CancellationToken stoppingToken = default)
    {
        return _processor.ProcessAsync(element.Id, stoppingToken);
    }
}