using AutoMapper;
using VogueUkraine.Identity.Data.Entities;
using VogueUkraine.Identity.Models.Requests;

namespace VogueUkraine.Identity.Mappers;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<CreateAppUserRequest, AppUserEntity>();
    }
}