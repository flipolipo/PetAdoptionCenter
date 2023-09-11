using SimpleWebDal.Models.Animal;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.DTOs;

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

