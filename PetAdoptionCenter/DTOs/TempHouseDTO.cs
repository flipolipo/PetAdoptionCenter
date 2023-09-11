using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.PetShelter;
using PetAdoptionCenter.Models.WebUser;

namespace PetAdoptionCenter.DTOs
{
    public class TempHouseDTO
    {
        public uint Id { get; init; }
        public UserDTO TemporaryOwner { get; set; }
        public Pet PetInTemporaryHouse { get; set; }
        public ShelterDTO ShelterName { get; set; }
        public DateTime StartOfTemporaryHouseDate { get; init; }
    }
}
