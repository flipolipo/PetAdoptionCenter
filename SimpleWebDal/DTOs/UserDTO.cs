using SimpleWebDal.Models.CalendarModel;
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
    public CalendarModelClass UsersTimeTable { get; set; }
    public IEnumerable<RoleDTO> Roles { get; set; }
}
