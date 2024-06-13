using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Data.Enums;
using VogueUkraine.Framework.Services.QueueService.Processor;
using VogueUkraine.Profile.Worker.Models;
using VogueUkraine.Profile.Worker.Repositories.Abstractions;
using VogueUkraine.Profile.Worker.Repositories.Queue;

namespace VogueUkraine.Profile.Worker.Queue;

public class FinishContestTaskQueueProcessor : QueueElementProcessor<FinishContestTask>
{
    private readonly IContestRepository _repository;

    public FinishContestTaskQueueProcessor(FinishContestTaskQueueRepository queue, IContestRepository repository) :
        base(queue)
    {
        _repository = repository;
    }

    protected override async Task<bool> ExecuteAsync(FinishContestTask element,
        CancellationToken stoppingToken = default)
    {
        try
        {
            await _repository.UpdateStatusAsync(new UpdateContestStatusRequest
            {
                Id = element.ContestId,
                Status = ContestStatus.Finished
            }, stoppingToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}