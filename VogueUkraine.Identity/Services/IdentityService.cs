using FluentValidation.Results;
using VogueUkraine.Framework;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Identity.Extensions;
using VogueUkraine.Identity.Helpers;
using VogueUkraine.Identity.Models;
using VogueUkraine.Identity.Models.Requests;
using VogueUkraine.Identity.Models.Responses;
using VogueUkraine.Identity.Options;
using VogueUkraine.Identity.Repository.Abstractions;
using VogueUkraine.Identity.Services.Abstractions;

namespace VogueUkraine.Identity.Services;

public class IdentityService : LogicalLayerElement, IIdentityService
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IAppUserTokensRepository _tokensRepository;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(IAppUserRepository appUserRepository, IAppUserTokensRepository tokensRepository,
        JwtOptions jwtOptions)
    {
        _appUserRepository = appUserRepository;
        _tokensRepository = tokensRepository;
        _jwtOptions = jwtOptions;
    }

    public async Task<ServiceResponse<ValidationResult>> SignUpAsync(SignUpModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var userExists = await _appUserRepository.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (userExists)
        {
            return ValidationFailure(new ValidationResult
            {
                Errors =
                {
                    new ValidationFailure(nameof(SignUpModelRequest.Email), ValidationMessages.UserAlreadyExists)
                }
            });
        }

        var createAppUserRequest = new CreateAppUserRequest
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = AuthHelper.HashPassword(request.Password)
        };

        await _appUserRepository.CreateAsync(createAppUserRequest, cancellationToken);

        return Success();
    }

    public async Task<ServiceResponse<SignInModelResponse, ValidationResult>> SignInAsync(SignInModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _appUserRepository.GetOneAsync(x => x.Email == request.Email, cancellationToken);
        if (user == null)
        {
            return NotFound<SignInModelResponse>();
        }

        if (!AuthHelper.VerifyPasswordHash(request.Password, user.PasswordHash))
        {
            return Failure<SignInModelResponse>(nameof(SignUpModelRequest.Password), "Password is incorrect",
                ServiceResponseStatuses.ValidationFailed);
        }

        var accessToken = AuthHelper.GenerateAccessToken(new GenerateAccessTokenModel
        {
            UserId = user.Id,
            Email = user.Email
        }, _jwtOptions);
        var refreshToken = AuthHelper.GenerateRefreshToken();

        await _tokensRepository.CreateAsync(new CreateAppUserTokenRequest
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        }, cancellationToken);

        return Success(new SignInModelResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    public async Task<ServiceResponse<SignInModelResponse, ValidationResult>> RefreshTokenAsync(
        RefreshTokenModelRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _appUserRepository.GetOneAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            return NotFound<SignInModelResponse>();
        }

        var refreshTokenModel = await _tokensRepository.GetOneAsync(request.UserId, cancellationToken);
        if (refreshTokenModel == null)
        {
            return NotFound<SignInModelResponse>();
        }

        if (request.RefreshToken != refreshTokenModel.Token)
        {
            return InvalidRefreshToken();
        }

        var accessToken = AuthHelper.GenerateAccessToken(new GenerateAccessTokenModel
        {
            UserId = user.Id,
            Email = user.Email
        }, _jwtOptions);
        var refreshToken = AuthHelper.GenerateRefreshToken();

        await _tokensRepository.CreateAsync(new CreateAppUserTokenRequest
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        }, cancellationToken);

        return Success(new SignInModelResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    private ServiceResponse<SignInModelResponse, ValidationResult> InvalidRefreshToken()
        => Failure<SignInModelResponse>("RefreshToken", "Invalid refresh token",
            ServiceResponseStatuses.ValidationFailed);
}