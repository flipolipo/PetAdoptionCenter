using AutoMapper;
using PetAdoptionCenter.DTOs;
using PetAdoptionCenter.Models.WebUser;

namespace SImpleWebLogic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDTO>();
        }
    }
}
