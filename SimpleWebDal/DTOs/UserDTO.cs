using SimpleWebDal.Models.Calendar;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.DTOs;
public class UserDTO
{
    [Key]
    public uint Id { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(15)]
    public string Username { get; set; }
    public BasicInformationDTO BasicInformation { get; set; }
    public TimeTable UsersTimeTable { get; set; }
    public IEnumerable<RoleDTO> Roles { get; set; }
}
