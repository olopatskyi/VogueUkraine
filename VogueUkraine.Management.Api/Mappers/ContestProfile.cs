using VogueUkraine.Data.Entities;
using VogueUkraine.Management.Api.Models.Requests;

namespace VogueUkraine.Management.Api.Mappers;

public class ContestProfile : AutoMapper.Profile
{
    public ContestProfile()
    {
        CreateMap<CreateContestModelRequest, Contest>();
    }
}