using VogueUkraine.Data.Entities;
using VogueUkraine.Management.Api.Models.Requests;
using VogueUkraine.Management.Api.Models.Responses;

namespace VogueUkraine.Management.Api.Mappers;

public class ParticipantProfile : AutoMapper.Profile
{
    public ParticipantProfile()
    {
        CreateMap<Participant, GetOneParticipantModelResponse>();
        
        CreateMap<CreateParticipantModelRequest, Participant>()
            .ForMember(x => x.Images, opt => opt.Ignore());
    }
}