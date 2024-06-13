namespace VogueUkraine.Profile.Api.Models.Requests;

public class AddParticipantsModelRequest
{
    public string ContestId { get; set; }

    public bool IncludeAllParticipants { get; set; }

    public IEnumerable<string> Participants { get; set; }
}