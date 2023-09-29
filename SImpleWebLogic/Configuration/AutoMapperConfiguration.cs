using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SImpleWebLogic.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                var profileTypes = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => typeof(Profile).IsAssignableFrom(t));

                foreach (var profileType in profileTypes)
                {
                    var profile = (Profile)Activator.CreateInstance(profileType);
                    cfg.AddProfile(profile);
                }
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
