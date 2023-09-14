
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.WebUser;

public class User
{

   
    public int UserId { get; set; }
  
    public string Username { get; set; }
 
    public string Password { get; set; }
   
    public BasicInformation BasicInformation { get; set; }
    public CalendarModelClass UserCalendar { get; set; }
    public ICollection<Role>? Roles { get; set; }

    //Navigation properties:

    public int ShelterId { get; set; }
    public Shelter Shelter { get; set; }
    public int TempHouseId { get; set; }
    public TempHouse TempHouse { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; }

    public int AdoptionId { get; set; }
    public Adoption Adoption { get; set; }

    // Foreign key for the first relationship (ShelterWorkers)
    public int ShelterWorkersId { get; set; }
    public Shelter ShelterWorkers { get; set; }

    // Foreign key for the second relationship (ShelterContributors)
    public int ShelterContributorsId { get; set; }
    public Shelter ShelterContributors { get; set; }
}
