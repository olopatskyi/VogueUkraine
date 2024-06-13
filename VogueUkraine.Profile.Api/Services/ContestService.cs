using FluentValidation.Results;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Profile.Api.Models;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;
using VogueUkraine.Profile.Api.Repositories.Abstractions;
using VogueUkraine.Profile.Api.Repositories.Queue;
using VogueUkraine.Profile.Api.Services.Abstractions;

namespace VogueUkraine.Profile.Api.Services;

public class ContestService : LogicalLayerElement, IContestService
{
    private readonly IContestRepository _contestRepository;
    private readonly IParticipantRepository _participantRepository;
    private readonly FinishContestTaskQueueRepository _finishContestTaskQueueRepository;

    public ContestService(IContestRepository contestRepository, IParticipantRepository participantRepository,
        FinishContestTaskQueueRepository finishContestTaskQueueRepository)
    {
        _contestRepository = contestRepository;
        _participantRepository = participantRepository;
        _finishContestTaskQueueRepository = finishContestTaskQueueRepository;
    }

    public async Task<ServiceResponse<ValidationResult>> CreateAsync(CreateContestModelRequest request,
        CancellationToken cancellationToken)
    {
        var contestExists = await _contestRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);
        if (contestExists)
        {
            return Failure(nameof(CreateContestModelRequest.Name), "Contest with same name already exists",
                ServiceResponseStatuses.Conflict);
        }

        var createResult = await _contestRepository.CreateAsync(request, cancellationToken);
        await _finishContestTaskQueueRepository.CreateAsync(new FinishContestTask
        {
            ContestId = createResult.ResourceId,
            DelayedTill = request.EndDate
        }, cancellationToken);

        return Success();
    }


    public async Task<ServiceResponse<GetOneContestModelResponse, ValidationResult>> GetByIdAsync(
        GetOneContestModelRequest request, CancellationToken cancellationToken)
    {
        var contest = await _contestRepository.GetOneAsync(request.Id, cancellationToken);
        var participantsDictionary =
            (await _participantRepository.GetManyByIdsAsync(contest.Participants.Select(x => x.Id), cancellationToken))
            .ToDictionary(x => x.Id);

        PopulateContestParticipants(contest.Participants, participantsDictionary);

        return Success(contest);
    }

    public Task<ServiceResponse<ValidationResult>> UpdateAsync(UpdateContestModelRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<ValidationResult>> AddParticipantsAsync(AddParticipantsModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var cursor = await _participantRepository.GetCursorAsync(1000, cancellationToken);
        var participantIds = new List<string>();

        if (request.IncludeAllParticipants)
        {
            while (await cursor.MoveNextAsync(cancellationToken))
            {
                var batch = cursor.Current;
                participantIds.AddRange(batch.Select(x => x.Id));
            }

            await _contestRepository.AddParticipantsAsync(new AddParticipantsModelRequest
            {
                ContestId = request.ContestId,
                Participants = participantIds
            }, cancellationToken);
        }
        else
        {
            await _contestRepository.AddParticipantsAsync(new AddParticipantsModelRequest
            {
                ContestId = request.ContestId,
                Participants = request.Participants
            }, cancellationToken);
        }


        return Success();
    }

    private static void PopulateContestParticipants(IEnumerable<ParticipantModel> participantModels,
        IReadOnlyDictionary<string, ParticipantModel> participantDictionary)
    {
        foreach (var participantModel in participantModels)
        {
            if (!participantDictionary.TryGetValue(participantModel.Id, out var participant))
                continue;

            participantModel.Name = participant.Name;
            participantModel.ImagesUrls = participant.ImagesUrls;
        }
    }
}