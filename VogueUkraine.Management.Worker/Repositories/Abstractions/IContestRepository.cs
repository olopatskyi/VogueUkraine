using VogueUkraine.Management.Worker.Models;

namespace VogueUkraine.Management.Worker.Repositories.Abstractions;

public interface IContestRepository
{
    Task UpdateStatusAsync(UpdateContestStatusRequest request, CancellationToken cancellationToken = default);

    Task<CreatedResourceModel> CreateAsync(CreateContestRequest request, CancellationToken cancellationToken = default);

    Task AddWinnerAsync(AddWinnerRequest request, CancellationToken cancellationToken = default);
}