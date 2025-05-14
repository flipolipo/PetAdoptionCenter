using AutoMapper;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.Models.WebUser;

namespace SImpleWebLogic.Profiles.MapWebUserProfile;

public class MapUser : Profile
{
    public MapUser()
    {
        CreateMap<User, UserReadDTO>();
        CreateMap<UserCreateDTO, User>();
    }
}
