using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using AopSample.Core.Commands;
using AopSample.Core.Interfaces.Commands;
using AopSample.Core.Interfaces.Forms;
using AopSample.Infrastructure.ObjectMapping;
using FluentValidation;
using FluentValidation.Results;
using Seterlund.CodeGuard;

namespace AopSample.Infrastructure.Web
{
    public class FormProcessor : IFormProcessor
    {
        public FormProcessor(IValidatorFactory validatorFactory, IModelMapper mapper, ICommandInvoker commandInvoker)
        {
            _validatorFactory = validatorFactory;
            _mapper = mapper;
            _commandInvoker = commandInvoker;
        }

        public ExecutionResult Process<TForm>(TForm form)
            where TForm : class
        {
            Guard.That(() => form).IsNotNull();
            _executionResult = new ExecutionResult(form);

            Validate(form);

            if (_executionResult.IsSuccessful)
            {
                dynamic command = _mapper.MapFormToCommand(form);
                _executionResult = _commandInvoker.Execute(command);
            }

            _logger.Info(_executionResult.ToString());
            return _executionResult;
        }

        private void Validate<T>(T form) where T : class
        {
            IValidator<T> validator = _validatorFactory.GetValidator<T>();
            if (validator == null)
                return;
            ValidationResult validationResult = validator.Validate(form);
            if (!validationResult.IsValid)
                _executionResult.AddErrors(Map(validationResult.Errors));
        }

        private IEnumerable<ValidatingFailure> Map(IList<ValidationFailure> fluentErrors)
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

        private IValidatorFactory _validatorFactory;
        readonly IModelMapper _mapper;
        readonly ICommandInvoker _commandInvoker;
        private ExecutionResult _executionResult;
    }
}
