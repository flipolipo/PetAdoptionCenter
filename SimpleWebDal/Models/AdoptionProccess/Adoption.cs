namespace SimpleWebDal.Models.AdoptionProccess;

public class Adoption
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public bool PreAdoptionPoll { get; set; }
    public bool ContractAdoption { get; set; }
    public bool Meetings { get; set; }
    public DateTime? DateOfAdoption { get; set; }
}
