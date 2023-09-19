using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.DTOs.ProfileUserDTOs;

public class ProfileModelReadDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User UserLogged { get; set; }
    public ICollection<PetReadDTO> ProfilePets { get; set; }
}
