
using PetAdoptionCenter.Models.TimeTable;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.Models.WebUser;

public class User
{
    [Key]
    [Required]
    public uint Id { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(15)]
    public string Username { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(15)]
    public string Password { get; set; }
    [Required]
    public BasicInformation BasicInformation { get; set; }
    public TimeTable<User> UsersTimeTable { get; set; }
    public IEnumerable<Role> Roles { get; set; }
}
