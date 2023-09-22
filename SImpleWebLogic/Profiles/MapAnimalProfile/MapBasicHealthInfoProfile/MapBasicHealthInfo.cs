using AutoMapper;
using SimpleWebDal.DTOs.AnimalDTOs.BasicHealthInfoDTOs;
using SimpleWebDal.Models.Animal;

namespace SImpleWebLogic.Profiles.MapAnimalProfile.MapBasicHealthInfoProfile;

public class MapBasicHealthInfo : Profile
{
    public MapBasicHealthInfo()
    {
        CreateMap<BasicHealthInfo, BasicHealthInfoReadDTO>();
        CreateMap<BasicHealthInfoCreateDTO, BasicHealthInfo>();
    }
}
