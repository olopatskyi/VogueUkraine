using FluentValidation.Results;
using Google.Apis.Auth;
using VogueUkraine.Framework;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Identity.Managers.Abstractions;
using VogueUkraine.Identity.Models.Requests;
using VogueUkraine.Identity.Models.Responses;
using VogueUkraine.Identity.Services.Abstractions;

namespace VogueUkraine.Identity.Managers;

public class IdentityManager : LogicalLayerElement, IIdentityManager
{
    private readonly IIdentityService _identityService;

    public IdentityManager(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ServiceResponse<ValidationResult>> SignUpAsync(SignUpModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var requestValidationResult = await new SignUpModelRequestValidator().ValidateAsync(request, cancellationToken);
        if (!requestValidationResult.IsValid)
        {
            return ValidationFailure(requestValidationResult);
        }

        // var googleValidationResult = await ValidateGoogleTokenAsync(request.GoogleToken);
        // if (!googleValidationResult.IsSuccess)
        // {
        //     return ValidationFailure(googleValidationResult.Errors);
        // }

        request.Email = Guid.NewGuid().ToString();
        var serviceResponse = await _identityService.SignUpAsync(request, cancellationToken);
        return serviceResponse;
    }

    public async Task<ServiceResponse<SignInModelResponse, ValidationResult>> SignInAsync(SignInModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await new SignInModelRequestValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationFailure<SignInModelResponse>(validationResult);
        }

        var serviceResponse = await _identityService.SignInAsync(request, cancellationToken);
        return serviceResponse;
    }

    public async Task<ServiceResponse<SignInModelResponse, ValidationResult>> RefreshTokenAsync(
        RefreshTokenModelRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = await new RefreshTokenModelRequestValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationFailure<SignInModelResponse>(validationResult);
        }

        var serviceResponse = await _identityService.RefreshTokenAsync(request, cancellationToken);
        return serviceResponse;
    }

    private async Task<ServiceResponse<GoogleJsonWebSignature.Payload, ValidationResult>>
        ValidateGoogleTokenAsync(string token)
    {
        try
        {
            var result = await GoogleJsonWebSignature.ValidateAsync(token);
            return Success(result);
        }
        catch (InvalidJwtException)
        {
            return ValidationFailure<GoogleJsonWebSignature.Payload>(new ValidationResult
            {
                Errors = new List<ValidationFailure>()
                {
                    new("GoogleToken", "Invalid Google ID token")
                }
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while verifying the token: {ex.Message}");
        }
    }
}