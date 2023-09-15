
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.ProfileUser;
using SimpleWebDal.Models.TemporaryHouse;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.WebUser;

public class User
{
    public int Id { get; set; }
    public Credentials Credentials { get; set; }
    public BasicInformation BasicInformation { get; set; }
    public CalendarActivity UserCalendar { get; set; }
    public ICollection<Role> Roles { get; set; }
    public TempHouse? TempHouse { get; set; }
    public ProfileModel? ProfileUser { get; set; }   
}
