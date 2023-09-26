namespace SimpleWebDal.Models.WebUser;

public class Role
{
    public Guid Id { get; set; }
    public RoleName RoleName { get; set; }
    public ICollection<User>? Users { get; set;}

}
