using System;
using Castle.Windsor;
using FluentValidation;

namespace AopSample.Infrastructure.Bootstrapper.Factories
{
    public class WindsorValidatorFactory : ValidatorFactoryBase
    {
        readonly IWindsorContainer _container;

        public WindsorValidatorFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _container.Resolve(validatorType) as IValidator;
        }
    }
}