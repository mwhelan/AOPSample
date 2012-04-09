using System.Web.Mvc;
using Castle.Windsor;
using AopSample.Infrastructure.Bootstrapper.Factories;
using AopSample.Infrastructure.Web.ModelMetadata;
using Seterlund.CodeGuard;

namespace AopSample.Infrastructure.Bootstrapper.StartupTasks
{
    public class MvcBootstrapper : IRunTaskAtStartup
    {
        private readonly IWindsorContainer _container;

        public MvcBootstrapper(IWindsorContainer container)
        {
            _container = container;
        }

        public void Execute()
        {
            //AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);

            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            IModelMetadataFilter[] filters = new IModelMetadataFilter[] { new PascalCaseToDisplayNameFilter() };
            ModelMetadataProviders.Current = new CustomModelMetadataProvider(filters);

#if DEBUG
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
#endif
        }

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Guard.That(() => filters).IsNotNull();

            filters.Add(new HandleErrorAttribute());
        }
    }
}
