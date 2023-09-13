using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.Calendar;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Animal;

public class Pet
{
    [Key]
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
