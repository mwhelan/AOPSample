using System;
using System.Linq;
using AutoMapper;

namespace AopSample.Infrastructure.ObjectMapping
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression IgnoreAllNonExisting(this IMappingExpression expression,
                                                              Type sourceType, Type destinationType)
        {
            var existingMaps = Mapper.GetAllTypeMaps()
                .First(x => x.SourceType.Equals(sourceType)
                            && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }

        /// <summary>
        /// http://cangencer.wordpress.com/2011/06/08/auto-ignore-non-existing-properties-with-automapper/
        /// </summary>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType)
                                                                  && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }
    }
}