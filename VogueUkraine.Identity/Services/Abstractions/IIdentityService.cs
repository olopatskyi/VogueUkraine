using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Identity.Models.Requests;

namespace VogueUkraine.Identity.Services.Abstractions;

public interface IIdentityService
{
    Task<ServiceResponse<ValidationResult>> CreateAsync(CreateUserModelRequest request,
        CancellationToken cancellationToken = default);
}