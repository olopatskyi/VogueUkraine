namespace VogueUkraine.Profile.Api.Models.Responses;

public class GetOneContestModelResponse
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public IEnumerable<ParticipantModel> Participants { get; set; }
}