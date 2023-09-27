using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SImpleWebLogic.Profiles.MapAddressProfile;
using SImpleWebLogic.Profiles.MapAdoptionProfile;
using SImpleWebLogic.Profiles.MapAnimalProfile;
using SImpleWebLogic.Profiles.MapAnimalProfile.MapBasicHealthInfoProfile;
using SImpleWebLogic.Profiles.MapAnimalProfile.MapDiseaseProfile;
using SImpleWebLogic.Profiles.MapAnimalProfile.MapUserProfiles;
using SImpleWebLogic.Profiles.MapAnimalProfile.MapVaccinationProfile;
using SImpleWebLogic.Profiles.MapCalendarProfile;
using SImpleWebLogic.Profiles.MapCalendarProfile.MapActivityProfile;
using SImpleWebLogic.Profiles.MapShelterProfile;
using SImpleWebLogic.Profiles.MapTemporaryHouseProfile;
using SImpleWebLogic.Profiles.MapWebUserProfile;
using SImpleWebLogic.Profiles.MapWebUserProfile.MapBasicInformationProfile;
using SImpleWebLogic.Profiles.MapWebUserProfile.MapCredentialsProfile;
using SImpleWebLogic.Profiles.MapWebUserProfile.MapRoleProfile;

namespace SImpleWebLogic.Configuration;

public static class AutoMapperConfiguration
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MapAddress());
            mc.AddProfile(new MapAdoption());
            mc.AddProfile(new MapVaccination());
            mc.AddProfile(new MapDisease());
            mc.AddProfile(new MapBasicHealthInfo());
            mc.AddProfile(new MapPet());
            mc.AddProfile(new MapActivity());
            mc.AddProfile(new MapCalendarActivity());
            mc.AddProfile(new MapShelter());
            mc.AddProfile(new MapTempHouse());
            mc.AddProfile(new MapBasicInformation());
            mc.AddProfile(new MapCredentials());
            mc.AddProfile(new MapRole());
            mc.AddProfile(new MapUser());
            mc.AddProfile(new MapPatronUsers());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
