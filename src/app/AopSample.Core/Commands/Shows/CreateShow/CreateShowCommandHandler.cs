using AopSample.Core.Domain;
using AopSample.Core.Interfaces.Commands;
using AopSample.Core.Interfaces.Data;

namespace AopSample.Core.Commands.Shows.CreateShow
{
    public class CreateShowCommandHandler : ICommandHandler<CreateShowCommand>
    {
        readonly IRepository<Show> _repository;
        public CreateShowCommand Command { get; set; }

        public CreateShowCommandHandler(IRepository<Show> repository)
        {
            _repository = repository;
        }

        public void Handle()
        {
            var show = Show.Create(Command.Name, Command.Overview);
            _repository.SaveOrUpdate(show);
        }
    }
}