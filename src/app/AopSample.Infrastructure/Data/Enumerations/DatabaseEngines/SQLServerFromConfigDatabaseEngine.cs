using System.Configuration;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using FluentNHibernate.Cfg.Db;

namespace AopSample.Infrastructure.Data.Enumerations.DatabaseEngines
{
    public abstract partial class DatabaseEngine
    {
        private class SQLServerFromConfigDatabaseEngine : DatabaseEngine
        {
            public SQLServerFromConfigDatabaseEngine()
                : base(5, "Sql Server from config")
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
                var connectionStringName = string.Format(ConnectionStrings.WebConfigConnectionStringNameFormat, projectType);

                var connectionStringElement = ConfigurationManager.ConnectionStrings[connectionStringName];
                if (connectionStringElement == null)
                {
                    const string messageFormat = "The '{0}' project has been configured to use a connection string in Web.Config named '{1}', but there is no such connection string.";
                    string message = string.Format(
                        messageFormat,
                        projectType,
                        connectionStringName);

                    throw new ConfigurationErrorsException(message);
                }

                return connectionStringElement.ConnectionString;
            }
        }
    }
}