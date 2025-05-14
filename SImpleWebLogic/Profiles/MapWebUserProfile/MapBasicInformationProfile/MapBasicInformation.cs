using AutoMapper;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;
using SimpleWebDal.Models.WebUser;

namespace SImpleWebLogic.Profiles.MapWebUserProfile.MapBasicInformationProfile;

public class MapBasicInformation : Profile

{
    public MapBasicInformation()
    {
        CreateMap<BasicInformation, BasicInformationReadDTO>();
        CreateMap<BasicInformationCreateDTO, BasicInformation>();
    }
}
