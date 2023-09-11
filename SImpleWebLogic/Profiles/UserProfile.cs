using AutoMapper;
using SimpleWebDal.DTOs;
using SimpleWebDal.Models.WebUser;

namespace SImpleWebLogic.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>();
    }
}
