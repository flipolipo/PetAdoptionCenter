
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
    //public IEnumerable<Role>? Roles { get; set; }


}
