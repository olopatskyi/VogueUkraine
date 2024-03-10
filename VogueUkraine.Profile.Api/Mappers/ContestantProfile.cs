using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;

namespace VogueUkraine.Profile.Api.Mappers;

public class ContestantProfile : AutoMapper.Profile
{
    public ContestantProfile()
    {
        CreateMap<Contestant, GetOneContestantModelResponse>();
        
        CreateMap<CreateContestantModelRequest, Contestant>()
            .ForMember(x => x.Images, opt => opt.Ignore());
    }
}