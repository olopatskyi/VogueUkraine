using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Data.Enums;
using VogueUkraine.Profile.Worker.Models;
using VogueUkraine.Profile.Worker.Repositories.Abstractions;
using VogueUkraine.Profile.Worker.Repositories.Queue;
using VogueUkraine.Profile.Worker.Services.Abstractions;
using VogueUkraine.Profile.Worker.Shared;

namespace VogueUkraine.Profile.Worker.Services;

public class FinishContestService : IFinishContestService
{
    private readonly IVoteRepository _voteRepository;
    private readonly CreateContestTaskQueueRepository _createContestTaskQueueRepository;
    private readonly IContestRepository _contestRepository;
    private readonly IParticipantRepository _participantRepository;

    public FinishContestService(IVoteRepository voteRepository,
        CreateContestTaskQueueRepository createContestTaskQueueRepository, IContestRepository contestRepository,
        IParticipantRepository participantRepository)
    {
        _voteRepository = voteRepository;
        _createContestTaskQueueRepository = createContestTaskQueueRepository;
        _contestRepository = contestRepository;
        _participantRepository = participantRepository;
    }

    public async Task FinishContestAsync(FinishContestRequest request, CancellationToken cancellationToken)
    {
        var votes = await _voteRepository.GetTopParticipantsAsync(request.ContestId, cancellationToken);

        var firstPlace = new List<string>();
        var secondPlace = new List<string>();
        var thirdPlace = new List<string>();

        var groupedVotes = votes.GroupBy(v => v.VotesCount)
            .OrderByDescending(g => g.Key)
            .ToList();
        var placeCounter = 0;

        foreach (var group in groupedVotes)
        {
            if (placeCounter == 0)
            {
                firstPlace.AddRange(group.Select(v => v.Id));
            }
            else if (placeCounter == 1)
            {
                secondPlace.AddRange(group.Select(v => v.Id));
            }
            else if (placeCounter == 2)
            {
                thirdPlace.AddRange(group.Select(v => v.Id));
                break;
            }

            placeCounter++;
        }

        var handlePlaceTasks = new List<Task>()
        {
            HandlePlaceAsync(firstPlace, 1, request, cancellationToken),
            HandlePlaceAsync(secondPlace, 2, request, cancellationToken),
            HandlePlaceAsync(thirdPlace, 3, request, cancellationToken)
        };
        await Task.WhenAll(handlePlaceTasks);
    }

    private async Task HandlePlaceAsync(List<string> placeParticipants, int placeNumber, FinishContestRequest request,
        CancellationToken cancellationToken)
    {
        if (placeParticipants.Count >= 2)
        {
            await _createContestTaskQueueRepository.CreateAsync(new CreateContestTask
            {
                Name = string.Format(MessageTemplates.AdditionalContestMessage, placeNumber, request.ContestName),
                Description = string.Empty,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                ParticipantsIds = placeParticipants,
                Status = ContestStatus.Scheduled
            }, cancellationToken);
        }
        else if (placeParticipants.Any() && placeNumber == 1)
        {
            var vote = await _voteRepository.GetOneAsync(new GetOneVoteRequest
            {
                ContestId = request.ContestId,
                ParticipantId = placeParticipants.First(),
            }, cancellationToken);

            var participant = await _participantRepository.GetOneAsync(placeParticipants.First(), cancellationToken);

            await _contestRepository.AddWinnerAsync(new AddWinnerRequest
            {
                ContestId = request.ContestId,
                ParticipantId = placeParticipants.First(),
                Name = participant.Name,
                VotesCount = vote.VotesCount
            }, cancellationToken);
        }
    }
}