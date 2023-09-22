using SimpleWebDal.DTOs.AnimalDTOs;

namespace SimpleWebDal.DTOs.AdoptionDTOs;

public class AdoptionCreateDTO
{
    public PetCreateDTO AdoptedPet { get; set; }
    public DateTime DateOfAdoption { get; set; }
}
