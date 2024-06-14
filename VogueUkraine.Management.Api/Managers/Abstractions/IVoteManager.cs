using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Management.Api.Models.Requests;

namespace VogueUkraine.Management.Api.Managers.Abstractions;

public interface IVoteManager
{
    Task<ServiceResponse<ValidationResult>> CreateVoteAsync(CreateVoteModelRequest request,
        CancellationToken cancellationToken = default);
}