using AopSample.Core.Commands;

namespace AopSample.Core.Interfaces.Commands
{
    public interface ICommandInvoker
    {
        ExecutionResult Execute<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
