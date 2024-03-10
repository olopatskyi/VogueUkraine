using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;

namespace VogueUkraine.Profile.Api.Services.Abstractions;

public interface IContestantService
{
    Task<ServiceResponse<ValidationResult>> CreateAsync(CreateContestantModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<ValidationResult>> DeleteAsync(DeleteContestantModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<GetManyContestantModelResponse, ValidationResult>> GetManyAsync(
        GetManyContestantsModelRequest request,
        CancellationToken cancellationToken = default);
}