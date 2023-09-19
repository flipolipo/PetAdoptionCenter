using AutoMapper;
using SimpleWebDal.DTOs.ShelterDTOs;
using SimpleWebDal.Models.PetShelter;

namespace SImpleWebLogic.Profiles.MapShelterProfile;

public class MapShelter : Profile
{
    public MapShelter()
    {
        CreateMap<Shelter, ShelterReadDTO>();
        CreateMap<ShelterCreateDTO, Shelter>();
    }
}
