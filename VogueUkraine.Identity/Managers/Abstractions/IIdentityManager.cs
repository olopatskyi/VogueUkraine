using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Identity.Models.Requests;
using VogueUkraine.Identity.Models.Responses;

namespace VogueUkraine.Identity.Managers.Abstractions;

public interface IIdentityManager
{
    Task<ServiceResponse<ValidationResult>> SignUpAsync(SignUpModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<SignInModelResponse, ValidationResult>> SignInAsync(SignInModelRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResponse<SignInModelResponse, ValidationResult>> RefreshTokenAsync(RefreshTokenModelRequest request,
        CancellationToken cancellationToken = default);
}