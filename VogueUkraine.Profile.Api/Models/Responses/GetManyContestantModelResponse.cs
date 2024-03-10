namespace VogueUkraine.Profile.Api.Models.Responses;

public class GetManyContestantModelResponse
{
    public IEnumerable<GetManyContestantModel> Data { get; set; }

    public int Total { get; set; }
}