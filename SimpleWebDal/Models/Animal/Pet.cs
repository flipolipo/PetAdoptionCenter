using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TimeTable;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Animal;

public class Pet
{
    [Key]
    [Required]
    public uint Id { get; init; }
    [Required]
    public PetType Type { get; init; }
    [Required]
    public BasicHealthInfo BasicHealthInfo { get; set; }

    public string Description { get; set; }
    public TimeTable<Pet> Callendar { get; set; }
    [Required]
    public PetStatus Status { get; set; }
    public Shelter? Shelter { get; set; }
    public bool AvaibleForAdoption { get; set; }
    public List<User> PatronUsers { get; set; }

   
}
