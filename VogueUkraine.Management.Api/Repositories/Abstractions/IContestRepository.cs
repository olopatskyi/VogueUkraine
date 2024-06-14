using System.Linq.Expressions;
using VogueUkraine.Data.Entities;
using VogueUkraine.Management.Api.Models;
using VogueUkraine.Management.Api.Models.Requests;
using VogueUkraine.Management.Api.Models.Responses;

namespace VogueUkraine.Management.Api.Repositories.Abstractions;

public interface IContestRepository
{
    Task<CreatedResourceModel> CreateAsync(CreateContestModelRequest request, CancellationToken cancellationToken = default);

    Task<GetOneContestModelResponse> GetOneAsync(string id, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<Contest, bool>> expression, CancellationToken cancellationToken = default);

    Task AddParticipantsAsync(AddParticipantsModelRequest request, CancellationToken cancellationToken = default);
}