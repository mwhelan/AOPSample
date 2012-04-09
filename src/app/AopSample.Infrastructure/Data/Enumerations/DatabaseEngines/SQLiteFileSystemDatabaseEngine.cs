using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using FluentNHibernate.Cfg.Db;

namespace AopSample.Infrastructure.Data.Enumerations.DatabaseEngines
{
    public abstract partial class DatabaseEngine
    {
        private class SQLiteFileSystemDatabaseEngine : DatabaseEngine
        {
            public SQLiteFileSystemDatabaseEngine()
                : base(3, "Sqlite File System")
            {
            }

            public override IPersistenceConfigurer PersistenceConfigurer(ProjectType projectType)
            {
                string connectionString = ConnectionStringFor(projectType);
                return SQLiteConfiguration
                    .Standard.UsingFile(connectionString)
                    .ShowSql()
                    .FormatSql()
                    .AdoNetBatchSize(100);
            }

            public override string ConnectionStringFor(ProjectType projectType)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}