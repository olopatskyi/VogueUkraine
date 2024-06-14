using AutoMapper;
using VogueUkraine.Data.Entities;
using VogueUkraine.Management.Worker.Models;

namespace VogueUkraine.Management.Worker.Mappers;

public class ContestProfile : Profile
{
    public ContestProfile()
    {
        CreateMap<CreateContestRequest, Contest>();
    }
}