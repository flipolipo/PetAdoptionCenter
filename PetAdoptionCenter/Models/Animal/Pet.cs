using PetAdoptionCenter.Models.Animal.Enums;
using PetAdoptionCenter.Models.Callendar;
using PetAdoptionCenter.Models.PetShelter;
using PetAdoptionCenter.Models.WebUser;

namespace PetAdoptionCenter.Models.Animal
{
    public class Pet
    {
        public uint Id { get; init; } 
        public PetType Type { get; init; }
        public BasicHealthInfo BasicHealthInfo { get; set; }
        public string Description { get; set; }
        public TimeTable Callendar { get; set; }
        public PetStatus Status { get; set; }
        public Shelter Shelter { get; set; }
        public bool AvaibleForAdoption { get; set; }
        public List<User> PatronUsers { get; set; }

       
    }
}
