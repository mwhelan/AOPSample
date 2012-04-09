using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Castle.Core.Logging;
using Castle.DynamicProxy;

namespace AopSample.Infrastructure.Logging
{
    public abstract class BaseMethodLogInterceptor : IInterceptor
    {
        public ILogger Logger { get; set; }
        private Stopwatch _stopwatch;

        public BaseMethodLogInterceptor()
        {
            _stopwatch = new Stopwatch();
        }

        public virtual void Intercept(IInvocation invocation)
        {
            try
            {
                if (!ShouldInterceptMethod(invocation)) return;
                BeforeInvoke(invocation);
                invocation.Proceed();
                AfterInvoke(invocation);
            }
            catch (Exception ex)
            {
                OnError(invocation, ex);
                throw;
            }
        }

        protected abstract bool ShouldInterceptMethod(IInvocation invocation);

        protected virtual void BeforeInvoke(IInvocation invocation)
        {
            if (ShouldInterceptMethod(invocation))
            {
                _stopwatch.Start();
                Logger.Debug(GetInputParametersFrom(invocation));
            }
        }

        protected virtual void AfterInvoke(IInvocation invocation)
        {
            if (ShouldInterceptMethod(invocation))
            {
                _stopwatch.Stop();
                string message = LoggingHelpers.MethodExitMessage(
                    GetMethodNameFrom(invocation),
                    _stopwatch.ElapsedMilliseconds,
                    invocation.ReturnValue);
                Logger.Debug(message);
            }
        }

        protected virtual void OnError(IInvocation invocation, Exception exception)
        {
            if (Logger.IsErrorEnabled)
            {
                Logger.Error(GetMethodNameFrom(invocation), exception);
            }

        }

        protected string GetInputParametersFrom(IInvocation invocation)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("=> {0}(", GetMethodNameFrom(invocation));
            foreach (object argument in invocation.Arguments)
            {
                string argumentDescription = GetArgumentDescription(argument);
                sb.Append(argumentDescription).Append(",");
            }
            if (invocation.Arguments.Count() > 0) sb.Length--;
            sb.Append(")");
            return sb.ToString();
        }

        protected string GetArgumentDescription(object argument)
        {
            if (argument == null)
                return "null";

            string description = string.Empty;
            try
            {
                description = argument.ConvertObjectToXmlString();
            }
            catch (Exception ex)
            {
                description = argument.ToString() + " [COULD NOT CONVERT]";
            }

            return description;
        }

        protected string GetMethodNameFrom(IInvocation invocation)
        {
            return string.Format("{0}.{1}", invocation.TargetType.Name, invocation.Method.Name);
        }
    }
}
