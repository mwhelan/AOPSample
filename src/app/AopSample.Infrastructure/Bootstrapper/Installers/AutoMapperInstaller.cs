using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using AopSample.Infrastructure.ObjectMapping;
using AopSample.Infrastructure.ObjectMapping.Profiles;
using AopSample.Infrastructure.Web.ActionResults;

namespace AopSample.Infrastructure.Bootstrapper.Installers
{
    public class AutoMapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ConfigureMapper(container);
            RegisterComponents(container);
            InitializeAutoMappedActionResults();

            Mapper.AssertConfigurationIsValid();
        }

        private void ConfigureMapper(IWindsorContainer container)
        {
            Mapper.Initialize(x => x.ConstructServicesUsing(container.Resolve));

            Mapper.AddProfile<AutoMapperDomainToViewModelProfile>();
            Mapper.AddProfile<AutoMapperFormModelToCommandProfile>();
            Mapper.AddProfile<AutoMapperCustomMappingsProfile>();
        }

        private void RegisterComponents(IWindsorContainer container)
        {
            container.Register(Component.For<IMappingEngine>().Instance(Mapper.Engine));
            container.Register(Component.For<IModelMapper>().ImplementedBy<ModelMapper>().LifestyleTransient());
        }

        private void InitializeAutoMappedActionResults()
        {
            AutoMappedViewResult.Map = Mapper.Map;
        }
    }
}
