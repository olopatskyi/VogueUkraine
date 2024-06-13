namespace VogueUkraine.Profile.Worker.Models;

public class AddWinnerRequest
{
    public string ContestId { get; set; }

    public string ParticipantId { get; set; }

    public string Name { get; set; }

    public int VotesCount { get; set; }
}