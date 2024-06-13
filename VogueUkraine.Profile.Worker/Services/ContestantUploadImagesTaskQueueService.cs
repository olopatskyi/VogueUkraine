using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Service;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;

namespace VogueUkraine.Profile.Worker.Services;

public class ContestantUploadImagesTaskQueueService : QueueService<ParticipantUploadImagesTask>
{
    private readonly ParticipantImagesUploadTaskProcessor _processor;

    public ContestantUploadImagesTaskQueueService(IQueueRepository<ParticipantUploadImagesTask> queue,
        ParticipantImagesUploadTaskProcessor processor) : base(queue)
    {
        _processor = processor;
    }

    protected override Task ProcessElementAsync(ParticipantUploadImagesTask element,
        CancellationToken stoppingToken = default)
    {
        return _processor.ProcessAsync(element.Id, stoppingToken);
    }
}