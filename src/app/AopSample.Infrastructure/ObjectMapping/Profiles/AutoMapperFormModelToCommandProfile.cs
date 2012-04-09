using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace AopSample.Infrastructure.ObjectMapping.Profiles
{
    public class AutoMapperFormModelToCommandProfile : Profile
    {
        protected override void Configure()
        {
            var types = Assembly.GetAssembly(typeof(IMapFromDomain<>)).GetExportedTypes();

            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapToCommand<>) &&
                            !t.IsAbstract &&
                                !t.IsInterface
                        select Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                var source = map.GetType();
                var destination = (from i in map.GetType().GetInterfaces()
                                   where i.IsGenericType &&
                                       i.GetGenericTypeDefinition() == typeof(IMapToCommand<>)
                                   select i.GetGenericArguments()[0]).Single();

                Mapper.CreateMap(source, destination)
                    .IgnoreAllNonExisting(source, destination);
            }
        }
    }
}