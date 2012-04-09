using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using AopSample.Core.Interfaces.Forms;
using AopSample.Infrastructure.Web;

namespace AopSample.Infrastructure.Bootstrapper.Installers
{
    public class FormProcessorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IFormProcessor>()
                    .ImplementedBy<FormProcessor>()
                    .LifestyleTransient());
        }
    }
}