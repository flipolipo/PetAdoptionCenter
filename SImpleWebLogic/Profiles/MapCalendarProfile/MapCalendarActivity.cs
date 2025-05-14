using AutoMapper;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.Models.CalendarModel;

namespace SImpleWebLogic.Profiles.MapCalendarProfile;

public class MapCalendarActivity : Profile
{
    public MapCalendarActivity()
    {
        CreateMap<CalendarActivity, CalendarActivityReadDTO>();
        CreateMap<CalendarActivityCreateDTO, CalendarActivity>();
    }
}
