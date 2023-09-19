using AutoMapper;
using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.Models.AdoptionProccess;

namespace SImpleWebLogic.Profiles.MapAdoptionProfile;

public class MapAdoption : Profile
{
    public MapAdoption()
    {
        CreateMap<Adoption, AddressReadDTO>();
        CreateMap<AddressCreateDTO, Adoption>();
    }
}
