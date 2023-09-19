using AutoMapper;
using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.Models.WebUser;

namespace SImpleWebLogic.Profiles.MapAddressProfile;

public class MapAddress : Profile
{
    public MapAddress()
    {
        CreateMap<Address, AddressReadDTO>();
        CreateMap<AddressCreateDTO, Address>();
    }
}
