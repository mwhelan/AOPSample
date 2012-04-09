using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Mvc;
using Shared.Core.Extensions;

namespace AopSample.Infrastructure.Logging
{
    public static class LoggingHelpers
    {
        public static string GetInputParametersFrom(ActionExecutingContext context)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("=> {0}(", GetActionNameFrom(context.ActionDescriptor));
            foreach (object argument in context.ActionParameters)
            {
                string argumentDescription = GetArgumentDescription(argument);
                sb.Append(argumentDescription).Append(",");
            }
            if (context.ActionParameters.Any())
                sb.Length--;
            sb.Append(")");
            return sb.ToString();
        }

        public static string GetArgumentDescription(object argument)
        {
            if (argument == null)
                return "null";

            if (argument.GetType().IsPrimitive)
                return argument.ToString();

            if (argument.GetType().CanBeCastTo<ViewResult>())
            {
                var viewResult = argument.As<ViewResult>();
                if (viewResult.ViewData.Model != null)
                {
                    return string.Format("{0}: {1}", viewResult.ViewName, viewResult.Model.GetType().Name);
                }
            }

            return argument.GetType().Name;
        }

        public static string GetActionNameFrom(ActionDescriptor descriptor)
        {
            return string.Format("{0}Controller.{1}", descriptor.ControllerDescriptor.ControllerName, descriptor.ActionName);
        }

        public static string ConvertObjectToXmlString<T>(this T instance)
        {
            var serializer = new DataContractSerializer(instance.GetType());
            using (var backing = new System.IO.StringWriter())
            using (var writer = new System.Xml.XmlTextWriter(backing))
            {
                serializer.WriteObject(writer, instance);
                return backing.ToString();
            }
        }

        public static string MethodExitMessage(string methodName, double durationInMilliseconds,
                                               object returnValue)
        {
            if (durationInMilliseconds > 0)
                durationInMilliseconds /= 1000;
            string message = string.Format("<= {0} in {1:0.00} secs. Result : {2}",
                                           methodName, durationInMilliseconds, returnValue);
            return message;
        }
    }
}