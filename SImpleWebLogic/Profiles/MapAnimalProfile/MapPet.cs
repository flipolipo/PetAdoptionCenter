using AutoMapper;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.Models.Animal;

namespace SImpleWebLogic.Profiles.MapAnimalProfile;

public class MapPet : Profile
{
    public MapPet()
    {
        CreateMap<Pet, PetReadDTO>();
        CreateMap<PetCreateDTO, Pet>();
    }
}
