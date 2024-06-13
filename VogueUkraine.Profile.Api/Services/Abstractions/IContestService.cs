using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;

namespace VogueUkraine.Profile.Api.Services.Abstractions;

public interface IContestService
{
    Task<ServiceResponse<ValidationResult>> CreateAsync(CreateContestModelRequest request,
        CancellationToken cancellationToken);

    Task<ServiceResponse<GetOneContestModelResponse, ValidationResult>> GetByIdAsync(GetOneContestModelRequest request,
        CancellationToken cancellationToken);

    Task<ServiceResponse<ValidationResult>> UpdateAsync(UpdateContestModelRequest request,
        CancellationToken cancellationToken);

    Task<ServiceResponse<ValidationResult>> AddParticipantsAsync(AddParticipantsModelRequest request,
        CancellationToken cancellationToken = default);
}