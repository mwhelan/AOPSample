using System.Web.Mvc;
using FluentSecurity;

namespace AopSample.Infrastructure.Security
{
    public class CustomHandleSecurityAttribute : HandleSecurityAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName;
            if (controllerName.EndsWith("Proxy"))
            {
                controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.BaseType.FullName;
            }

            var overrideResult = SecurityHandler.HandleSecurityFor(controllerName, actionName);
            if (overrideResult != null) filterContext.Result = overrideResult;
        }
    }
}
