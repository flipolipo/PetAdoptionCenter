namespace SimpleWebDal.Models.WebUser;

public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
