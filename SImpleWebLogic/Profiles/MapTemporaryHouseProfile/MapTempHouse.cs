using AutoMapper;
using SimpleWebDal.DTOs.TemporaryHouseDTOs;
using SimpleWebDal.Models.TemporaryHouse;

namespace SImpleWebLogic.Profiles.MapTemporaryHouseProfile;

public class MapTempHouse : Profile
{
    public MapTempHouse()
    {
        CreateMap<TempHouse, TempHouseReadDTO>();
        CreateMap<TempHouseCreateDTO, TempHouse>();
    }
}
