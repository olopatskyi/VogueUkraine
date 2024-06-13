using FluentValidation.Results;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Profile.Api.Extensions;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;
using VogueUkraine.Profile.Api.Repositories.Abstractions;
using VogueUkraine.Profile.Api.Services.Abstractions;

namespace VogueUkraine.Profile.Api.Services;

public class ParticipantService : LogicalLayerElement, IParticipantService
{
    private readonly IParticipantRepository _repository;
    private readonly IContestantUploadImagesTaskRepository _uploadImagesTaskRepository;
    private readonly IDeleteS3FilesTaskRepository _deleteS3FilesTaskRepository;

    public ParticipantService(IContestantUploadImagesTaskRepository uploadImagesTaskRepository,
        IParticipantRepository repository, IDeleteS3FilesTaskRepository deleteS3FilesTaskRepository)
    {
        _uploadImagesTaskRepository = uploadImagesTaskRepository;
        _repository = repository;
        _deleteS3FilesTaskRepository = deleteS3FilesTaskRepository;
    }

    public async Task<ServiceResponse<ValidationResult>> CreateAsync(CreateParticipantModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.CreateAsync(request, cancellationToken);

        await _uploadImagesTaskRepository.CreateAsync(new ParticipantUploadImagesTask
        {
            UserId = result.Id,
            Files = await request.Images.ConvertToByteArrayCollectionAsync()
        }, cancellationToken);

        return Success();
    }

    public async Task<ServiceResponse<ValidationResult>> DeleteAsync(DeleteParticipantModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var contestant = await _repository.GetOneAsync(request.Id, cancellationToken);
        if (contestant == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(request.Id, cancellationToken);

        await _deleteS3FilesTaskRepository.CreateAsync(new DeleteS3FileTask
        {
            FilesIds = contestant.Images
        }, cancellationToken);

        return Success();
    }

    public Task<ServiceResponse<GetManyContestantModelResponse, ValidationResult>> GetManyAsync(
        GetManyParticipantsModelRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}