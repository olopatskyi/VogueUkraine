using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage;

namespace VogueUkraine.Management.Worker.Repositories.Queue;

public class FinishContestTaskQueueRepository : QueueRepository<FinishContestTask>
{
    public FinishContestTaskQueueRepository(VogueUkraineContext context) : base(context.FinishContestTasks)
    {
    }
}