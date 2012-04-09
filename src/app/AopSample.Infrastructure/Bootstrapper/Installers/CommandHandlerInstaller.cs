using AopSample.Infrastructure.Logging;
using AopSample.Core.Commands.Shows.CreateShow;
using AopSample.Core.Interfaces.Commands;
using AopSample.Infrastructure.Bootstrapper.Factories;
using AopSample.Infrastructure.Commands;

using Castle.Core;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AopSample.Infrastructure.Bootstrapper.Installers
{
    public class CommandHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>()
                .Register(
                    Component.For<WindsorCommandHandlerSelector>()
                        .ImplementedBy<WindsorCommandHandlerSelector>(),
                    TypesToRegister
                        .BasedOn(typeof(ICommandHandler<>))
                        .WithService.Base()
                        .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)),
                    Component.For<ICommandHandlerFactory>()
                        .AsFactory(c => c.SelectedWith<WindsorCommandHandlerSelector>())
                        .LifeStyle.Transient);
            container.Register(Component.For<ICommandInvoker>()
                                   .ImplementedBy<CommandInvoker>()
                                   .LifeStyle.Transient
                                   .Interceptors<CommandHandlerLogInterceptor>());
        }

        FromAssemblyDescriptor _typesToRegister = AllTypes.FromAssemblyContaining<CreateShowCommandHandler>();

        public FromAssemblyDescriptor TypesToRegister
        {
            get { return _typesToRegister; }
            set { _typesToRegister = value; }
        }
    }
}