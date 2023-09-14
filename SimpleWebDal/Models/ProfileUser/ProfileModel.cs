using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Models.ProfileUser;

public class ProfileModel
{
    public int ProfileId { get; set; }
    public User UserLogged { get; set; }
    public ICollection<Pet> ProfilePets { get; set; }
}
