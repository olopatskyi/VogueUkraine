using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Data.Enums;
using VogueUkraine.Framework.Services.QueueService.Processor;
using VogueUkraine.Management.Worker.Models;
using VogueUkraine.Management.Worker.Repositories.Abstractions;
using VogueUkraine.Management.Worker.Repositories.Queue;
using VogueUkraine.Management.Worker.Services.Abstractions;

namespace VogueUkraine.Management.Worker.Queue;

public class FinishContestTaskQueueProcessor : QueueElementProcessor<FinishContestTask>
{
    private readonly IContestRepository _repository;
    private readonly IFinishContestService _finishContestService;

    public FinishContestTaskQueueProcessor(FinishContestTaskQueueRepository queue, IContestRepository repository,
        IFinishContestService finishContestService) :
        base(queue)
    {
        _repository = repository;
        _finishContestService = finishContestService;
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

            await _finishContestService.FinishContestAsync(new FinishContestRequest
            {
                ContestId = element.ContestId,
                ContestName = element.ContestName
            }, stoppingToken);
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}