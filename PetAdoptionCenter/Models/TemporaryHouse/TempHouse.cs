using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.PetShelter;
using PetAdoptionCenter.Models.WebUser;

namespace PetAdoptionCenter.Models.TemporaryHouse
{
    public class TempHouse
    {
        public uint Id { get; init; }
        public User TemporaryOwner { get; set; }
        public Pet PetInTemporaryHouse { get; set; }
        public Shelter ShelterName { get; set; }
        public DateTime StartOfTemporaryHouseDate { get; init; }
    }
}
