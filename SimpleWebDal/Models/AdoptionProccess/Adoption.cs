namespace SimpleWebDal.Models.AdoptionProccess;

public class Adoption
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public Guid UserId { get; set; }
    public bool IsPreAdoptionPoll { get; set; }
    public bool IsContractAdoption { get; set; }
    public bool IsMeetings { get; set; }
    public DateTime? DateOfAdoption { get; set; }
}
