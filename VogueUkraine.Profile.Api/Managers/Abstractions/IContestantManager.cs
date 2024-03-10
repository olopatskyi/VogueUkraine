using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;

namespace VogueUkraine.Profile.Api.Managers.Abstractions;

public interface IContestantManager
{
    Task<ServiceResponse<ValidationResult>> CreateAsync(CreateContestantModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<ValidationResult>> DeleteAsync(DeleteContestantModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<ServiceResponse<GetManyContestantModelResponse>>> GetManyAsync(
        GetManyContestantsModelRequest request, CancellationToken cancellationToken = default);
}