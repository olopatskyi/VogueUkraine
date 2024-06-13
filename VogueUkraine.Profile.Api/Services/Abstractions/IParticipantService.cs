using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;

namespace VogueUkraine.Profile.Api.Services.Abstractions;

public interface IParticipantService
{
    Task<ServiceResponse<ValidationResult>> CreateAsync(CreateParticipantModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<ValidationResult>> DeleteAsync(DeleteParticipantModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<GetManyContestantModelResponse, ValidationResult>> GetManyAsync(
        GetManyParticipantsModelRequest request,
        CancellationToken cancellationToken = default);
}