using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;

namespace VogueUkraine.Profile.Api.Managers.Abstractions;

public interface IParticipantManager
{
    Task<ServiceResponse<ValidationResult>> CreateAsync(CreateParticipantModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<ValidationResult>> DeleteAsync(DeleteParticipantModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<ServiceResponse<GetManyContestantModelResponse>>> GetManyAsync(
        GetManyParticipantsModelRequest request, CancellationToken cancellationToken = default);
}