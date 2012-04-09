
using Castle.DynamicProxy;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AopSample.Infrastructure.Bootstrapper.Installers
{
    public class LoggingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.Null));

            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IInterceptor>()
                    .LifestyleTransient());
        }
    }
}
