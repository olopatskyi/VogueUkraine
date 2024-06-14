namespace VogueUkraine.Management.Api.Models.Responses;

public class GetManyContestantModelResponse
{
    public IEnumerable<GetManyContestantModel> Data { get; set; }

    public int Total { get; set; }
}