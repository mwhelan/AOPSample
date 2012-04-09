using AopSample.Core.Domain;
using AopSample.Core.Interfaces.Commands;
using AopSample.Core.Interfaces.Data;

namespace AopSample.Core.Commands.Shows.EditShow
{
    public class EditShowCommandHandler : ICommandHandler<EditShowCommand>
    {
        readonly IRepository<Show> _repository;
        public EditShowCommand Command { get; set; }

        public EditShowCommandHandler(IRepository<Show> repository)
        {
            _repository = repository;
        }

        public void Handle()
        {
            var show = _repository.Get(Command.Id);
            show.Edit(Command.Name, Command.Overview);
            _repository.SaveOrUpdate(show);
        }
    }
}