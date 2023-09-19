using AutoMapper;
using SimpleWebDal.DTOs.WebUserDTOs.CredentialsDTOs;
using SimpleWebDal.Models.WebUser;

namespace SImpleWebLogic.Profiles.MapWebUserProfile.MapCredentialsProfile;

public class MapCredentials : Profile
{
    public MapCredentials()
    {
        CreateMap<Credentials, CredentialsReadDTO>();
        CreateMap<CredentialsCreateDTO, Credentials>();
    }
}
