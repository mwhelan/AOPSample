using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace AopSample.Infrastructure.Data.Enumerations.DatabaseEngines
{
    public abstract partial class DatabaseEngine
    {
        public class SQLiteInMemoryDatabaseEngine : DatabaseEngine
        {
            public SQLiteInMemoryDatabaseEngine()
                : base(4, "Sqlite In Memory")
            {
            }

            public override IPersistenceConfigurer PersistenceConfigurer(ProjectType projectType)
            {
                var sqLiteConfiguration = SQLiteConfiguration
                    .Standard.InMemory()
                    .Raw(Environment.ConnectionProvider, typeof(SqliteCachedConnectionProvider).AssemblyQualifiedName)
                    .ShowSql()
                    .FormatSql()
                    .AdoNetBatchSize(100);

                return sqLiteConfiguration;
            }

            public override string ConnectionStringFor(ProjectType projectType)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}