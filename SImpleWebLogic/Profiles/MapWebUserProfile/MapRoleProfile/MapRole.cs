using AutoMapper;
using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;
using SimpleWebDal.Models.WebUser;

namespace SImpleWebLogic.Profiles.MapWebUserProfile.MapRoleProfile;

public class MapRole : Profile
{
    public MapRole()
    {
        CreateMap<Role, RoleReadDTO>();
        CreateMap<RoleCreateDTO, Role>();
    }
}
