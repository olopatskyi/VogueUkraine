using VogueUkraine.Profile.Worker.Models;

namespace VogueUkraine.Profile.Worker.Services.Abstractions;

public interface IFinishContestService
{
    Task FinishContestAsync(FinishContestRequest request, CancellationToken cancellationToken);
}