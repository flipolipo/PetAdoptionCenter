using SimpleWebDal.DTOs.AnimalDTOs;

namespace SimpleWebDal.DTOs.AdoptionDTOs;

public class AdoptionReadDTO
{
    public Guid Id { get; set; }
    public PetReadDTO AdoptedPet { get; set; }
    public DateTime DateOfAdoption { get; set; }
}
