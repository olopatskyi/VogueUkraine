using FluentValidation.Results;
using VogueUkraine.Framework;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Profile.Api.Managers.Abstractions;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;
using VogueUkraine.Profile.Api.Services.Abstractions;

namespace VogueUkraine.Profile.Api.Managers;

public class ContestantManager : LogicalLayerElement, IContestantManager
{
    private readonly IContestantService _service;

    public ContestantManager(IContestantService service)
    {
        _service = service;
    }

    public async Task<ServiceResponse<ValidationResult>> CreateAsync(CreateContestantModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await new CreateContestantRequestValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationFailure(validationResult);
        }

        await _service.CreateAsync(request, cancellationToken);

        return Success();
    }

    public async Task<ServiceResponse<ValidationResult>> DeleteAsync(DeleteContestantModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await new DeleteContestantRequestValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationFailure(validationResult);
        }

        await _service.DeleteAsync(request, cancellationToken);
        return Success();
    }

    public Task<ServiceResponse<ServiceResponse<GetManyContestantModelResponse>>> GetManyAsync(
        GetManyContestantsModelRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}