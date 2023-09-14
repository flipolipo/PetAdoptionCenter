using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.ProfileUser;
using SimpleWebDal.Models.AdoptionProccess;

namespace SimpleWebDal.Models.Animal;

public class Pet
{

    public int PetId { get; init; }
    public PetType Type { get; init; }
    public BasicHealthInfo BasicHealthInfo { get; set; }

    public string Description { get; set; }
    public CalendarModelClass Callendar { get; set; }
    public PetStatus Status { get; set; }
    
    public bool AvaibleForAdoption { get; set; }
    public ICollection<User> PatronUsers { get; set; }

   
}
