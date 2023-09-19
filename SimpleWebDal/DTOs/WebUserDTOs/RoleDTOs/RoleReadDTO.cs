namespace SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;

public class RoleReadDTO
{
    public string RoleName { get; set; }
    public ICollection<UserReadDTO> Users { get; set; }
}
