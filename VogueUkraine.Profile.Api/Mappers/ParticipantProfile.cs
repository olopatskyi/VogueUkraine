using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;

namespace VogueUkraine.Profile.Api.Mappers;

public class ParticipantProfile : AutoMapper.Profile
{
    public ParticipantProfile()
    {
        CreateMap<Participant, GetOneParticipantModelResponse>();
        
        CreateMap<CreateParticipantModelRequest, Participant>()
            .ForMember(x => x.Images, opt => opt.Ignore());
    }
}