using AutoMapper;
using SimpleWebDal.DTOs.AnimalDTOs.PatronUsersDTOs;
using SimpleWebDal.Models.Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SImpleWebLogic.Profiles.MapAnimalProfile.MapUserProfiles
{
    public class MapPatronUsers : Profile
    {
        public MapPatronUsers() 
        {
            CreateMap<PatronUsers, PatronUsersReadDTO>();
            CreateMap<PatronUsersCreateDTO, PatronUsers>();
        }
    }
}
