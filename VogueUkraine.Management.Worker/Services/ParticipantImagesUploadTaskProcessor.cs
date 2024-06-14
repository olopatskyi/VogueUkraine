using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Processor;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;
using VogueUkraine.Management.Worker.Models;
using VogueUkraine.Management.Worker.Repositories.Abstractions;
using VogueUkraine.Management.Worker.Services.Abstractions;

namespace VogueUkraine.Management.Worker.Services;

public class ParticipantImagesUploadTaskProcessor : QueueElementProcessor<ParticipantUploadImagesTask>
{
    private readonly IS3Service _service;
    private readonly IParticipantRepository _participantRepository;

    public ParticipantImagesUploadTaskProcessor(IQueueRepository<ParticipantUploadImagesTask> queue, IS3Service service,
        IParticipantRepository participantRepository) :
        base(queue)
    {
        _service = service;
        _participantRepository = participantRepository;
    }

    protected override async Task<bool> ExecuteAsync(ParticipantUploadImagesTask element,
        CancellationToken stoppingToken = default)
    {
        try
        {
            var images = await _service.AddFilesAsync(element.Files, stoppingToken);
            await _participantRepository.UpdateAsync(new UpdateContestantModelRequest
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