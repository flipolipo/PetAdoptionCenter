using AutoMapper;
using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;
using SimpleWebDal.Models.CalendarModel;

namespace SImpleWebLogic.Profiles.MapCalendarProfile.MapActivityProfile;

public class MapActivity : Profile
{
    public MapActivity()
    {
        CreateMap<Activity, ActivityReadDTO>();
        CreateMap<ActivityCreateDTO, Activity>();
    }
}
