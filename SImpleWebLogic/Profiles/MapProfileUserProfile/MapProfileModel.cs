using AutoMapper;
using SimpleWebDal.DTOs.ProfileUserDTOs;
using SimpleWebDal.Models.ProfileUser;

namespace SImpleWebLogic.Profiles.MapProfileUserProfile;

public class MapProfileModel : Profile
{
    public MapProfileModel()
    {
        CreateMap<ProfileModel, ProfileModelReadDTO>();
        CreateMap<ProfileModelCreateDTO, ProfileModel>();
    }
}
