using SimpleWebDal.Models.WebUser.Enums;

namespace SimpleWebDal.Models.WebUser;

public class Role
{
    public Guid Id { get; set; }
    public RoleName Title { get; set; }
}
