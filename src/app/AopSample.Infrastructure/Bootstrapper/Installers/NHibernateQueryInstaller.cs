using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using AopSample.Core.Interfaces.Data;
using AopSample.Infrastructure.Data.Mapping;

namespace AopSample.Infrastructure.Bootstrapper.Installers
{
    public class NHibernateQueryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types.FromAssemblyContaining<NHibernateConfigurationBuilder>()
                    .BasedOn(typeof(IQuery<>))
                    .WithService.Base()
                    .LifestyleTransient());
        }
    }
}