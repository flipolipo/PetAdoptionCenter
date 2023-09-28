using AutoMapper;
using SimpleWebDal.DTOs.WebUserDTOs.UserPetsDTOs;
using SimpleWebDal.Models.WebUser;

namespace SImpleWebLogic.Profiles.MapUserPetsProfile;

public class MapUserPets : Profile
{
    public MapUserPets()
    {
        CreateMap<UserPets, UserPetsReadDTO>();
        CreateMap<UserPetsCreateDTO, UserPets>();
    }
}
