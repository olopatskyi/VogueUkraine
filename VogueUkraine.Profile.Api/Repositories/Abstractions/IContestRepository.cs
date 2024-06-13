using System.Linq.Expressions;
using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Api.Models;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;

namespace VogueUkraine.Profile.Api.Repositories.Abstractions;

public interface IContestRepository
{
    Task<CreatedResourceModel> CreateAsync(CreateContestModelRequest request, CancellationToken cancellationToken = default);

    Task<GetOneContestModelResponse> GetOneAsync(string id, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<Contest, bool>> expression, CancellationToken cancellationToken = default);
}