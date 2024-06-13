namespace VogueUkraine.Profile.Worker.Models;

public class CreateContestRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public IEnumerable<string> ParticipantsIds { get; set; }
}