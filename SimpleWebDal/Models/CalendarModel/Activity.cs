using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.CalendarModel;

public class Activity
{

    
    public int ActivityId { get; set; }
   
    public string Name { get; set; }
  
    public DateTime AcctivityDate { get; set; }
   public Pet Pet { get; set; }

}
