using System;
using System.Reflection;
using Castle.Facilities.TypedFactory;
using AopSample.Core.Interfaces.Commands;

namespace AopSample.Infrastructure.Bootstrapper.Factories
{
    public class WindsorCommandHandlerSelector : DefaultTypedFactoryComponentSelector
    {
        protected override Type GetComponentType(MethodInfo method, object[] arguments)
        {
            var command = arguments[0];
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            return handlerType;
        }
    }
}