using VogueUkraine.Profile.Worker.Models;

namespace VogueUkraine.Profile.Worker.Repositories.Abstractions;

public interface IVoteRepository
{
    Task<List<TopParticipantModel>> GetTopParticipantsAsync(string contestId,
        CancellationToken cancellationToken = default);

    Task<List<VoteModel>> GetManyAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);
    
    Task<VoteModel> GetOneAsync(GetOneVoteRequest request, CancellationToken cancellationToken = default);
}