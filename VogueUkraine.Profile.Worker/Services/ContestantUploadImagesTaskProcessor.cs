using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Processor;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;
using VogueUkraine.Profile.Worker.Models;
using VogueUkraine.Profile.Worker.Repositories.Abstractions;
using VogueUkraine.Profile.Worker.Services.Abstractions;

namespace VogueUkraine.Profile.Worker.Services;

public class ContestantUploadImagesTaskProcessor : QueueElementProcessor<ContestantUploadImagesTask>
{
    private readonly IS3Service _service;
    private readonly IContestantRepository _contestantRepository;

    public ContestantUploadImagesTaskProcessor(IQueueRepository<ContestantUploadImagesTask> queue, IS3Service service,
        IContestantRepository contestantRepository) :
        base(queue)
    {
        _service = service;
        _contestantRepository = contestantRepository;
    }

    protected override async Task<bool> ExecuteAsync(ContestantUploadImagesTask element,
        CancellationToken stoppingToken = default)
    {
        try
        {
            var images = await _service.AddFilesAsync(element.Files, stoppingToken);
            await _contestantRepository.UpdateAsync(new UpdateContestantModelRequest
            {
                UserId = element.UserId,
                Images = images,
            }, stoppingToken);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}