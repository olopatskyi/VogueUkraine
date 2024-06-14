using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Management.Api.Models.Requests;

namespace VogueUkraine.Management.Api.Services.Abstractions;

public interface IVoteService
{
    Task<ServiceResponse<ValidationResult>> CreateVoteAsync(CreateVoteModelRequest request,
        CancellationToken cancellationToken = default);
}