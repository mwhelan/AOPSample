using System.Web.Mvc;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using FluentSecurity;

namespace AopSample.Infrastructure.Logging
{
    public class ControllerLogInterceptor : IInterceptor
    {
        public ILogger Logger { get; set; }

        public void Intercept(IInvocation invocation)
        {
            switch (invocation.Method.Name)
            {
                case "OnActionExecuting":
                    OnActionExecuting(invocation);
                    break;
                case "OnActionExecuted":
                    OnActionExecuted(invocation);
                    break;
            }

            invocation.Proceed();
            //try
            //{
            //}
            //catch (Exception ex)
            //{
            //    _logger.ErrorFormat(ex, "ControllerLogInterceptor tried to intercept {0} and threw {1} exception.",
            //        invocation.Method.Name, ex.GetType().ToString());
            //}
        }

        private void OnActionExecuting(IInvocation invocation)
        {
            var context = invocation.Arguments[0] as ActionExecutingContext;
            var message = string.Format("Started controller action {0}.{1}", invocation.TargetType.Name, context.ActionDescriptor.ActionName);
            Logger.Debug(message);
        }

        private void OnActionExecuted(IInvocation invocation)
        {
            var context = invocation.Arguments[0] as ActionExecutedContext;
            var message = string.Format("Ended controller action {0}.{1}", invocation.TargetType.Name, context.ActionDescriptor.ActionName);
            Logger.Debug(message);
        }

        //private bool HandleSecurity(ActionExecutingContext filterContext)
        //{
        //    bool allowed = true;
        //    var actionName = filterContext.ActionDescriptor.ActionName;
        //    var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName;

        //    var overrideResult = _securityHandler.HandleSecurityFor(controllerName, actionName);
        //    if (overrideResult != null)
        //    {
        //        filterContext.Result = overrideResult;
        //        allowed = false;
        //    }
        //    return allowed;
        //}

    }
}