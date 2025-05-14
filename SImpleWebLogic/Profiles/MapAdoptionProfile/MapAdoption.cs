using AutoMapper;
using SimpleWebDal.DTOs.AdoptionDTOs;
using SimpleWebDal.Models.AdoptionProccess;

namespace SImpleWebLogic.Profiles.MapAdoptionProfile;

public class MapAdoption : Profile
{
    public MapAdoption()
    {
        CreateMap<Adoption, AdoptionReadDTO>();
        CreateMap<AdoptionCreateDTO, Adoption>();
    }
}
