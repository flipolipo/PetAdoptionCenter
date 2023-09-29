using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Models.TemporaryHouse;

public class TempHouse
{

    public Guid Id { get; set; }
    public string UserId { get; set; }
    public User TemporaryOwner { get; set; }
    public Guid AddressId { get; set; }
    public Address TemporaryHouseAddress { get; set; }
    public ICollection<Pet>? PetsInTemporaryHouse { get; set; }
    public DateTime StartOfTemporaryHouseDate { get; set; }
}
