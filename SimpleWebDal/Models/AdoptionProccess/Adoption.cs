using SimpleWebDal.Models.Animal;

namespace SimpleWebDal.Models.AdoptionProccess;

public class Adoption
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public Pet AdoptedPet { get; set; }
    public DateTime DateOfAdoption { get; set; }
}
