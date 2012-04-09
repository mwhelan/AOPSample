using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using AopSample.Core.Commands.Shows.CreateShow;
using AopSample.Infrastructure.Bootstrapper.Factories;
using AopSample.Infrastructure.Web.ViewModels.Shows;
using FluentValidation;
using Shared.Core.Extensions;

namespace AopSample.Infrastructure.Bootstrapper.Installers
{
    public class FluentValidationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IValidatorFactory>().ImplementedBy<WindsorValidatorFactory>());

            AssemblyScanner.FindValidatorsInAssemblyContaining<CreateShowFormValidator>()
                .Each(result => container.Register(Component.For((Type) result.InterfaceType)
                                                       .ImplementedBy(result.ValidatorType)));

            AssemblyScanner.FindValidatorsInAssemblyContaining<CreateShowCommandValidator>()
                .Each(result => container.Register(Component.For(result.InterfaceType)
                                                       .ImplementedBy(result.ValidatorType)));

        }
    }
}