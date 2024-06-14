using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Processor;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;
using VogueUkraine.Management.Worker.Services.Abstractions;

namespace VogueUkraine.Management.Worker.Services;

public class DeleteS3FilesTaskProcessor : QueueElementProcessor<DeleteS3FileTask>
{
    private readonly IS3Service _service;

    public DeleteS3FilesTaskProcessor(IQueueRepository<DeleteS3FileTask> queue, IS3Service service) : base(queue)
    {
        _service = service;
    }

    protected override async Task<bool> ExecuteAsync(DeleteS3FileTask element,
        CancellationToken stoppingToken = default)
    {
        try
        {
            await _service.DeleteFilesAsync(element.FilesIds, stoppingToken);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}