using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.ShelterDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.AdoptionDTOs;

public class AdoptionCreateDTO
{
    public PetCreateDTO AdoptedPet { get; set; }
    public ShelterCreateDTO Shelter { get; set; }
    public UserCreateDTO Adopter { get; set; }
    public DateTime DateOfAdoption { get; set; }
}
