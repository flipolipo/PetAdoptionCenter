using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.ShelterDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.AdoptionDTOs;

public class AdoptionReadDTO
{
    public Guid Id { get; set; }
    public PetReadDTO PetToAdoption { get; set; }
    public ShelterReadDTO Shelter { get; set; }
    public UserReadDTO Adopter { get; set; }
    public DateTime DateOfAdoption { get; set; }
}
