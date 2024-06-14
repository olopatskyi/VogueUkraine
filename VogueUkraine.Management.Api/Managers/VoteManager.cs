using FluentValidation.Results;
using VogueUkraine.Framework;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Management.Api.Managers.Abstractions;
using VogueUkraine.Management.Api.Models.Requests;
using VogueUkraine.Management.Api.Services.Abstractions;

namespace VogueUkraine.Management.Api.Managers;

public class VoteManager : LogicalLayerElement, IVoteManager
{
    private readonly IVoteService _service;

    public VoteManager(IVoteService service)
    {
        _service = service;
    }

    public async Task<ServiceResponse<ValidationResult>> CreateVoteAsync(CreateVoteModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await new CreateVoteModelRequestValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return ValidationFailure(validationResult);

        var serviceResponse = await _service.CreateVoteAsync(request, cancellationToken);
        return serviceResponse;
    }
}