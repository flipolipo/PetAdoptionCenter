using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Models.AdoptionProccess;

public class Adoption
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public Pet AdoptedPet { get; set; }
    public DateTime DateOfAdoption { get; set; }
}
