using FluentValidation.Results;
using VogueUkraine.Framework;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Management.Api.Models.Requests;
using VogueUkraine.Management.Api.Repositories.Abstractions;
using VogueUkraine.Management.Api.Services.Abstractions;

namespace VogueUkraine.Management.Api.Services;

public class VoteService : LogicalLayerElement, IVoteService
{
    private readonly IVoteRepository _repository;

    public VoteService(IVoteRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ValidationResult>> CreateVoteAsync(CreateVoteModelRequest request, CancellationToken cancellationToken = default)
    {
        await _repository.CreateVoteAsync(request, cancellationToken);
        return Success();
    }
}