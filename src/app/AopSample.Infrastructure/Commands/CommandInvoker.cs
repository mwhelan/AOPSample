using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using AopSample.Core.Commands;
using AopSample.Core.Interfaces.Commands;
using FluentValidation;
using FluentValidation.Results;
using Seterlund.CodeGuard;

namespace AopSample.Infrastructure.Commands
{
    public class CommandInvoker : ICommandInvoker
    {
        public CommandInvoker(ICommandHandlerFactory commandHandlerFactory, IValidatorFactory validatorFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
            _validatorFactory = validatorFactory;
        }

        public ExecutionResult Execute<T>(T command) where T : class, ICommand
        {
            Guard.That(() => command).IsNotNull();
            _executionResult = new ExecutionResult(command);

            Validate(command);

            if (_executionResult.IsSuccessful)
            {
                ProcessCommand(command);
            }

            _logger.Info(_executionResult.ToString());
            return _executionResult;
        }

        private void ProcessCommand<T>(T command) where T : class, ICommand
        {
            try
            {
                HandlerFor(command).Handle();
            }
            catch (ValidatingException validatingException)
            {
                _executionResult.AddErrors(validatingException.Errors);
            }
        }

        private void Validate<T>(T command) where T : class, ICommand
        {
            IValidator<T> validator = _validatorFactory.GetValidator<T>();
            if (validator == null)
                return;
            ValidationResult validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
                _executionResult.AddErrors(Map(validationResult.Errors));
            //throw new ValidatingException(Map(validationResult.Errors));
        }

        private ICommandHandler<T> HandlerFor<T>(T command) where T : class, ICommand
        {
            var handler = (ICommandHandler<T>)_commandHandlerFactory.HandlerForCommand(command);
            if (handler == null)
                throw new CommandHandlerNotFoundException(typeof(T));
            return handler;
        }

        private IEnumerable<ValidatingFailure> Map(IList<FluentValidation.Results.ValidationFailure> fluentErrors)
        {
            return fluentErrors
                 .Select(error => new ValidatingFailure(error.PropertyName, error.ErrorMessage))
                 .ToList();
        }

        private ILogger _logger = NullLogger.Instance;
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        private ICommandHandlerFactory _commandHandlerFactory;
        private IValidatorFactory _validatorFactory;
        private ExecutionResult _executionResult;
    }
}
