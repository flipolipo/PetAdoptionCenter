namespace SimpleWebDal.Models.Animal;

public class PatronUsers
{
    public Guid Id { get; set; }
    public List<string> Patrons { get; set; }
    public Guid PetId { get; set; }
}
