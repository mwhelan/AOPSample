using System;
using AopSample.Core.Commands;
using Castle.DynamicProxy;

namespace AopSample.Infrastructure.Logging
{
    public class CommandHandlerLogInterceptor : BaseMethodLogInterceptor
    {
        public override void Intercept(IInvocation invocation)
        {
            BeforeInvoke(invocation);

            try
            {
                invocation.Proceed();
                ExecutionResult result = invocation.ReturnValue as ExecutionResult;
                if (result.IsSuccessful && Logger.IsDebugEnabled)
                {
                    AfterInvoke(invocation);
                }
                else
                {
                    if (Logger.IsInfoEnabled)
                    {
                        Logger.Info(result.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                if (Logger.IsErrorEnabled)
                {
                    Logger.Error(GetMethodNameFrom(invocation), ex);
                }
                throw;
            }
        }

        protected override bool ShouldInterceptMethod(IInvocation invocation)
        {
            return invocation.Method.Name == "Execute";
        }
    }
}