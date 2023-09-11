using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.PetShelter;
using PetAdoptionCenter.Models.TimeTable;
using PetAdoptionCenter.Models.WebUser;

namespace PetAdoptionCenter.DTOs
{
    public class ShelterDTO
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public Address ShelterAddress { get; set; }
        public string ShelterDescription { get; set; }
        public UserDTO ShelterOwner { get; set; }
        public IEnumerable<UserDTO> UsersAdmin { get; set; }
        public IEnumerable<UserDTO> UsersHelping { get; set; }
        public IEnumerable<Pet> ListOfPets { get; set; }
        public IEnumerable<Pet> ListOfPetsAdopted { get; set; }
        
    }
}
