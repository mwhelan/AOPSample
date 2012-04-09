using System.Collections.Generic;
using AopSample.Core.Interfaces.Commands;

namespace AopSample.Infrastructure.ObjectMapping
{
    public interface IModelMapper
    {
        ICommand MapFormToCommand(object form);
        TDestination Map<TSource, TDestination>(TSource source);
        IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> source);

    }
}
