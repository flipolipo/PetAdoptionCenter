using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.PetShelter;
using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.DTOs
{
    public class TempHouseDTO
    {
        [Key]
        public uint Id { get; init; }
        [Required]
        public UserDTO TemporaryOwner { get; set; }
        [Required]
        public Pet PetInTemporaryHouse { get; set; }
        [Required]
        public ShelterDTO ShelterName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartOfTemporaryHouseDate { get; init; }
    }
}
