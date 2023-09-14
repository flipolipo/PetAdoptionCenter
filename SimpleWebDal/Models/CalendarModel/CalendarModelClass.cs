using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.CalendarModel;
public class CalendarModelClass
{ 

    public int CalendarId { get; init; }

    public DateTime DateWithTime { get; set; }

    public ICollection<Activity> Activities { get; set; }

    //Navigation properties:
    public int ShelterId { get; set; }
    public Shelter Shelter { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int PetId { get; set; }
    public Pet Pet { get; set; }

}
