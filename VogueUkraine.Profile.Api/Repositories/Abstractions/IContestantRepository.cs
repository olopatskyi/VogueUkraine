using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;

namespace VogueUkraine.Profile.Api.Repositories.Abstractions;

public interface IContestantRepository
{
    Task<GetOneContestantModelResponse> GetOneAsync(string id, CancellationToken cancellationToken = default);

    Task<CreateResourceModelResponse> CreateAsync(CreateContestantModelRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string id, CancellationToken cancellationToken);

    Task<GetManyContestantModelResponse> GetManyAsync(GetManyContestantsModelRequest request,
        CancellationToken cancellationToken = default);
}