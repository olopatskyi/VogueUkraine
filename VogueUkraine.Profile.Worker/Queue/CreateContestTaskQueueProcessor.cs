using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Processor;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;
using VogueUkraine.Profile.Worker.Models;
using VogueUkraine.Profile.Worker.Repositories.Abstractions;
using VogueUkraine.Profile.Worker.Repositories.Queue;

namespace VogueUkraine.Profile.Worker.Queue;

public class CreateContestTaskQueueProcessor : QueueElementProcessor<CreateContestTask>
{
    private readonly IContestRepository _contestRepository;
    private readonly FinishContestTaskQueueRepository _finishContestTaskQueueRepository;
    
    public CreateContestTaskQueueProcessor(CreateContestTaskQueueRepository queue,
        IContestRepository contestRepository, FinishContestTaskQueueRepository finishContestTaskQueueRepository) : base(queue)
    {
        _contestRepository = contestRepository;
        _finishContestTaskQueueRepository = finishContestTaskQueueRepository;
    }

    protected override async Task<bool> ExecuteAsync(CreateContestTask element,
        CancellationToken stoppingToken = default)
    {
        try
        {
            var createResult = await _contestRepository.CreateAsync(new CreateContestRequest
            {
                Name = element.Name,
                Description = element.Description,
                StartDate = element.StartDate,
                EndDate = element.EndDate,
                ParticipantsIds = element.ParticipantsIds
            }, stoppingToken);

            await _finishContestTaskQueueRepository.CreateAsync(new FinishContestTask
            {
                ContestId = createResult.ResourceId,
                DelayedTill = element.EndDate
            }, stoppingToken);
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}