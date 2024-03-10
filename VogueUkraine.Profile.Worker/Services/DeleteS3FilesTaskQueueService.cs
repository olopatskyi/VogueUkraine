using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Service;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;

namespace VogueUkraine.Profile.Worker.Services;

public class DeleteS3FilesTaskQueueService : QueueService<DeleteS3FileTask>
{
    private readonly DeleteS3FilesTaskProcessor _processor;

    public DeleteS3FilesTaskQueueService(IQueueRepository<DeleteS3FileTask> queue, DeleteS3FilesTaskProcessor processor)
        : base(queue)
    {
        _processor = processor;
    }

    protected override Task ProcessElementAsync(DeleteS3FileTask element, CancellationToken stoppingToken = default)
    {
        return _processor.ProcessAsync(element.Id, stoppingToken);
    }
}