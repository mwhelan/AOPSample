using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AopSample.Core.Interfaces.Commands;

namespace AopSample.Infrastructure.ObjectMapping
{
    public class ModelMapper : IModelMapper
    {
        readonly IMappingEngine _mapper;

        public ModelMapper(IMappingEngine mapper)
        {
            _mapper = mapper;
        }

        public ICommand MapFormToCommand(object form)
        {
            var sourceType = form.GetType();
            TypeMap typeMap = Mapper
                .GetAllTypeMaps()
                .SingleOrDefault(map => map.SourceType == sourceType);
            
            if (typeMap == null)
            {
                throw new ArgumentException(string.Format("There is no Command associated with Form {0}", form.GetType().Name));
            }

            Type destinationType = typeMap.DestinationType;
            return (ICommand)_mapper.Map(form, sourceType, destinationType);
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public virtual IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return source.Select(item => _mapper.Map<TSource, TDestination>(item));
        }

    }
}