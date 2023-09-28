namespace SimpleWebDal.Models.WebUser;

public class UserPets
{
    public Guid Id { get; set; }
    public List<string>? Pets { get; set; }
    public Guid UserId { get; set; }
}
