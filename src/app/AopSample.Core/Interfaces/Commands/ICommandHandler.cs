namespace AopSample.Core.Interfaces.Commands
{
    public interface ICommandHandler
    {
        void Handle();
    }

    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : ICommand
    {
        TCommand Command { get; set; }
    }
}
