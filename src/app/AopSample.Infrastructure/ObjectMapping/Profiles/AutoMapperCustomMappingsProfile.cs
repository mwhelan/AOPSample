using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace AopSample.Infrastructure.ObjectMapping.Profiles
{
    public class AutoMapperCustomMappingsProfile : Profile
    {
        protected override void Configure()
        {
            var types = Assembly.GetAssembly(typeof(IMapFromDomain<>)).GetExportedTypes();

            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                            !t.IsAbstract &&
                                !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }

        }
    }
}