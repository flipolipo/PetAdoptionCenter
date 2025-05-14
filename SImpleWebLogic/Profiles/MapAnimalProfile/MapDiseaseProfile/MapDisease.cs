using AutoMapper;
using SimpleWebDal.DTOs.AnimalDTOs.DiseaseDTOs;
using SimpleWebDal.Models.Animal;

namespace SImpleWebLogic.Profiles.MapAnimalProfile.MapDiseaseProfile;

public class MapDisease : Profile
{
    public MapDisease()
    {
        CreateMap<Disease, DiseaseReadDTO>();
        CreateMap<DiseaseCreateDTO, Disease>();
    }
}
