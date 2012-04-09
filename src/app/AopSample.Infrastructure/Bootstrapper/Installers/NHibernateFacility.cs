using System;
using System.Diagnostics;
using Castle.Core;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Extensions;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using AopSample.Infrastructure.Data.Mapping;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace AopSample.Infrastructure.Bootstrapper.Installers
{
    public class NHibernateFacility : AbstractFacility
    {
        private static IConfiguration _configuration;

        protected override void Init()
        {
            _configuration = Kernel.Resolve<IConfiguration>();
            var config = new NHibernateConfigurationBuilder(PersistenceConfigurer()).Build();

            Kernel.Register(Component.For<ISessionFactory>()
                                .UsingFactoryMethod(config.BuildSessionFactory)
                                .LifeStyle.Singleton,
                            Component.For<ISession>()
                                .UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                                .LifeStyle.Is(SessionLifeStyle.Invoke()));
        }

        public static Func<LifestyleType> SessionLifeStyle = () =>
        {
            return LifestyleType.PerWebRequest;
        };

        public static Func<IPersistenceConfigurer> PersistenceConfigurer = () =>
        {
            //throw new ArgumentException("A PersistenceConfigurer must be set.");
            var injectedProjectType = Process.GetCurrentProcess().GetProjectType();
            return _configuration
                .DatabaseEngineFor(injectedProjectType ?? ProjectType.Application)
                .PersistenceConfigurer(injectedProjectType ?? ProjectType.Application);
        };
    }
}
