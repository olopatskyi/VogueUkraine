using FluentValidation.Results;
using VogueUkraine.Framework;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Profile.Api.Managers.Abstractions;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;
using VogueUkraine.Profile.Api.Services.Abstractions;

namespace VogueUkraine.Profile.Api.Managers;

public class ContestManager : LogicalLayerElement, IContestManager
{
    private readonly IContestService _contestService;

    public ContestManager(IContestService contestService)
    {
        _contestService = contestService;
    }

    public async Task<ServiceResponse<ValidationResult>> CreateAsync(CreateContestModelRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await new CreateContestModelRequestValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationFailure(validationResult);
        }

        var serviceResponse = await _contestService.CreateAsync(request, cancellationToken);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetOneContestModelResponse, ValidationResult>> GetByIdAsync(
        GetOneContestModelRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await new GetOneContestModelRequestValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationFailure<GetOneContestModelResponse>(validationResult);
        }

        var serviceResponse = await _contestService.GetByIdAsync(request, cancellationToken);
        return serviceResponse;
    }

    public async Task<ServiceResponse<ValidationResult>> UpdateAsync(UpdateContestModelRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await new UpdateContestModelRequestValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationFailure(validationResult);
        }

        var serviceResponse = await _contestService.UpdateAsync(request, cancellationToken);
        return serviceResponse;
    }

    public async Task<ServiceResponse<ValidationResult>> AddParticipantsAsync(AddParticipantsModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var serviceResponse = await _contestService.AddParticipantsAsync(request, cancellationToken);
        return serviceResponse;
    }
}