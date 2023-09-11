using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.PetShelter;
using PetAdoptionCenter.Models.TimeTable;
using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.DTOs
{
    public class ShelterDTO
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        public Address ShelterAddress { get; set; }
        public string ShelterDescription { get; set; }
        [Required]
        public UserDTO ShelterOwner { get; set; }
        public IEnumerable<UserDTO> ShelterWorkers { get; set; }
        public IEnumerable<UserDTO> ShelterContributors { get; set; }
        public IEnumerable<Pet> ListOfPets { get; set; }
        public IEnumerable<Pet> ListOfPetsAdopted { get; set; }
        
    }
}
