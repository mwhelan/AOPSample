using System;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using FluentNHibernate.Cfg.Db;

namespace AopSample.Infrastructure.Data.Enumerations.DatabaseEngines
{
    public abstract partial class DatabaseEngine
    {
        public class SqlServerDatabaseEngine : DatabaseEngine
        {

            public SqlServerDatabaseEngine()
                : base(2, "Sql Server")
            {
            }

            public override IPersistenceConfigurer PersistenceConfigurer(ProjectType projectType)
            {
                string connectionString = ConnectionStringFor(projectType);
                return MsSqlConfiguration
                    .MsSql2008
                    .ConnectionString(connectionString)
                    .ShowSql()
                    .FormatSql()
                    .AdoNetBatchSize(100);
            }

            public override string ConnectionStringFor(ProjectType projectType)
            {
                return string.Format(ConnectionStrings.SqlServerConnectionStringFormat, ServerName, projectType);
            }

            string _serverName;
            public string ServerName
            {
                get
                {
                    if (string.IsNullOrEmpty(_serverName))
                    {
                        throw new ArgumentNullException(_serverName);
                    }
                    return _serverName;
                }
                set { _serverName = value; }
            }
        }
    }
}