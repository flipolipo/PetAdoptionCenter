using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Models.TemporaryHouse;

public class TempHouse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User TemporaryOwner { get; set; }
    public Guid AddressId { get; set; }
    public Address TemporaryHouseAddress { get; set; }
    public ICollection<Pet> PetsInTemporaryHouse { get; set; }
    public bool IsPreTempHousePoll { get; set; }
    public string TempHousePoll { get; set; }
    public Guid? CalendarId { get; set; }
    public CalendarActivity? Activity { get; set; }
    public bool IsMeetings { get; set; }
    public DateTimeOffset StartOfTemporaryHouseDate { get; set; }
}
