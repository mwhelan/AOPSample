using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using AopSample.Infrastructure.Bootstrapper.Installers;
using AopSample.Infrastructure.Bootstrapper.StartupTasks;
using Shared.Core.Extensions;

namespace AopSample.Infrastructure.Bootstrapper
{
    public static class ApplicationConfigurator
    {
        private static IWindsorContainer container;

        public static void BuildContainer()
        {
            container = new WindsorContainer();
            container.AddFacility<ConfigurationFacility>();
            container.Install(FromAssembly.This());
            container.Register(Component.For<IWindsorContainer>().Instance(container));
            container.AddFacility<NHibernateFacility>();
        }

        public static void Start()
        {
            var tasks = container.ResolveAll<IRunTaskAtStartup>();
            tasks.Each(task => task.Execute());
        }

        public static void End()
        {
            container.Dispose();
        }
    }
}
