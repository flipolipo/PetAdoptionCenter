using SimpleWebDal.Models.WebUser.Enums;

namespace SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;

public class RoleReadDTO
{
    public Guid Id { get; set; }
    public RoleName Title { get; set; }
}
