using PetAdoptionCenter.Models.TimeTable;
using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.DTOs;

public class UserDTO
{
    [Key]
    public uint Id { get; set; }
    public string Username { get; set; }
    public BasicInformationDTO BasicInformation { get; set; }
    public TimeTable<UserDTO> UsersTimeTable { get; set; }
    public IEnumerable<RoleDTO> Roles { get; set; }
}
