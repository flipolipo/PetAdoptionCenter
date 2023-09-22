using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.ProfileUserDTOs;

public class ProfileModelReadDTO
{
    public Guid Id { get; set; }
    public UserReadDTO UserLogged { get; set; }
    public ICollection<PetReadDTO>? ProfilePets { get; set; }
}
