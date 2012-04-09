namespace AopSample.Core.Interfaces.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler HandlerForCommand(ICommand command);
    }

}