using AopSample.Core.Interfaces.Commands;

namespace AopSample.Infrastructure.ObjectMapping
{
    public interface IMapToCommand<T> where T : ICommand
    {
    }
}