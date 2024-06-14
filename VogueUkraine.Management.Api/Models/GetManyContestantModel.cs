namespace VogueUkraine.Management.Api.Models;

public class GetManyContestantModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public IEnumerable<string> Images { get; set; }
}