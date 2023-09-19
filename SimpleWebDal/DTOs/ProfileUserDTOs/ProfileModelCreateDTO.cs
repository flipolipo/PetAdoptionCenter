using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.ProfileUserDTOs;

public class ProfileModelCreateDTO
{
    public Guid UserId { get; set; }
    public UserCreateDTO UserLogged { get; set; }
    public ICollection<PetCreateDTO> ProfilePets { get; set; }
}
