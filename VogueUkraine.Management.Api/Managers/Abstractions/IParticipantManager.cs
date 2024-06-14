using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Management.Api.Models.Requests;
using VogueUkraine.Management.Api.Models.Responses;

namespace VogueUkraine.Management.Api.Managers.Abstractions;

public interface IParticipantManager
{
    Task<ServiceResponse<ValidationResult>> CreateAsync(CreateParticipantModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<ValidationResult>> DeleteAsync(DeleteParticipantModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<ServiceResponse<GetManyContestantModelResponse>>> GetManyAsync(
        GetManyParticipantsModelRequest request, CancellationToken cancellationToken = default);
}