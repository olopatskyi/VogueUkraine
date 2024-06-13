using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Api.Models.Requests;

namespace VogueUkraine.Profile.Api.Mappers;

public class ContestProfile : AutoMapper.Profile
{
    public ContestProfile()
    {
        CreateMap<CreateContestModelRequest, Contest>();
    }
}