using VogueUkraine.Data.Models;
using VogueUkraine.Management.Worker.Models;

namespace VogueUkraine.Management.Worker.Repositories.Abstractions;

public interface IParticipantRepository
{
    Task<List<ParticipantWinnerModel>> GetManyByIdsAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default);
    
    Task UpdateAsync(UpdateContestantModelRequest request, CancellationToken cancellationToken = default);

    Task<ParticipantModel> GetOneAsync(string id, CancellationToken cancellationToken = default);
}