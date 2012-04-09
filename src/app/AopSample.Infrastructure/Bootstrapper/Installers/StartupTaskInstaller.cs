using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using AopSample.Infrastructure.Bootstrapper.StartupTasks;

namespace AopSample.Infrastructure.Bootstrapper.Installers
{
    public class StartupTaskInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly()
                                   .BasedOn<IRunTaskAtStartup>()
                                   .WithService.Base());
        }
    }
}