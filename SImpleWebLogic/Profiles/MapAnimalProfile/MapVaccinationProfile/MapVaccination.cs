using AutoMapper;
using SimpleWebDal.DTOs.AnimalDTOs.VaccinationDTOs;
using SimpleWebDal.Models.Animal;

namespace SImpleWebLogic.Profiles.MapAnimalProfile.MapVaccinationProfile;

public class MapVaccination : Profile
{
    public MapVaccination()
    {
        CreateMap<Vaccination, VaccinationReadDTO>();
        CreateMap<VaccinationCreateDTO, Vaccination>();
    }
}
