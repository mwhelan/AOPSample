using AopSample.Core.Domain;
using AopSample.Infrastructure.Data.Mapping.Conventions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace AopSample.Infrastructure.Data.Mapping
{
    public class NHibernateConfigurationBuilder
    {
        private readonly IPersistenceConfigurer _databaseConfig;

        public NHibernateConfigurationBuilder(IPersistenceConfigurer databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public Configuration Build()
        {
            return GetFluentConfiguration().BuildConfiguration();
        }

        public FluentConfiguration GetFluentConfiguration()
        {
            var model = BuildAutoMap();
            return Fluently.Configure()
                .Database(_databaseConfig)
                .Mappings(configuration => configuration.AutoMappings.Add(model));
        }

        private AutoPersistenceModel BuildAutoMap()
        {
            var result = AutoMap.AssemblyOf<Entity>(new AutoMappingConfiguration())
                .Conventions.AddFromAssemblyOf<DefaultConventions>()
                .Alterations(alt => alt.AddFromAssemblyOf<DefaultConventions>())
                .UseOverridesFromAssemblyOf<DefaultConventions>()
                //.OverrideAll(x => x.IgnoreProperties("CommitteeType", "SortName"))
                .IgnoreBase<Entity>();
            return result;
        }
    }
}
