using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AopSample.Core.Commands;
using AopSample.Core.Interfaces.Forms;
using Shared.Core.Extensions;

namespace AopSample.Infrastructure.Web.ActionResults
{
    public class FormActionResult<TForm> : ActionResult
        where TForm : class
    {
        private readonly IFormProcessor _formProcessor;

        public TForm Form { get; private set; }
        public ActionResult Success { get; set; }
        public ActionResult Failure { get; set; }
        public ActionResult ActionThatWasInvoked { get; set; }
        public Dictionary<string, Func<object>> FailureActions { get; set; }

        public FormActionResult(TForm form, IFormProcessor formProcessor)
        {
            _formProcessor = formProcessor;
            Form = form;
            FailureActions = new Dictionary<string, Func<object>>();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            ExecutionResult result = _formProcessor.Process<TForm>(Form);
            if (result.IsSuccessful)
            {
                ActionThatWasInvoked = Success;
                Success.ExecuteResult(context);
            }
            else
            {
                ActionThatWasInvoked = Failure;
                context.Controller.ViewData.ModelState.AddErrorsToModelState(new ValidatingException(result.Errors));
                FailureActions.Each(action => context.Controller.ViewData[action.Key] = action.Value.Invoke());
                Failure.ExecuteResult(context);
            }
        }
    }
}
