using System;
using AopSample.Infrastructure.Bootstrapper;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Extensions;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using FluentNHibernate.Cfg.Db;

namespace AopSample.Infrastructure.Data.Enumerations.DatabaseEngines
{
    public abstract partial class DatabaseEngine
    {
        private class SqlCEDatabaseEngine : DatabaseEngine
        {
            public SqlCEDatabaseEngine()
                : base(6, "Sql Server Compact Edition")
            {
            }

            public override IPersistenceConfigurer PersistenceConfigurer(ProjectType projectType)
            {
                string connectionString = ConnectionStringFor(projectType);
                return MsSqlCeConfiguration
                    .Standard
                    .ConnectionString(connection => connection.Is(connectionString))
                    .ShowSql()
                    .FormatSql()
                    //.Dialect("NHibernate.Dialect.MsSqlCe40Dialect")
                    .Dialect<MsSqlCe40DialectWithVariableLimit>()
                    .AdoNetBatchSize(100);
            }

            public override string ConnectionStringFor(ProjectType projectType)
            {
                string appDataPath = string.Empty;
                if (projectType == ProjectType.Application)
                    appDataPath = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
                if (string.IsNullOrEmpty(appDataPath))
                    appDataPath = ConnectionStrings.GetProjectPath(ConfigSettings.WebAssemblyName) + @"\App_Data";

                string dbPath = string.Format("{0}\\EpisodeGuide_{1}.sdf", appDataPath, projectType);
                string connectionString = string.Format("Data Source={0};Persist Security Info=False;", dbPath);

                return connectionString;
            }
        }
    }
}