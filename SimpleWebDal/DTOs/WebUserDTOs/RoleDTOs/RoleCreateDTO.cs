namespace SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;

public class RoleCreateDTO
{
    public string RoleName { get; set; }
    public ICollection<UserCreateDTO> Users { get; set; }
}
