using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using AopSample.Infrastructure.Data.Enumerations.DatabaseEngines;
using Seterlund.CodeGuard;

namespace AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration
{
    public class Project
    {
        public DatabaseEngine DatabaseEngine { get; protected set; }
        public ProjectType ProjectType { get; private set; }

        public Project(ProjectType projectType) : this(projectType, DatabaseEngine.Null)
        {
        }

        public Project(ProjectType projectType, DatabaseEngine databaseEngine)
        {
            ProjectType = projectType;
            DatabaseEngine = databaseEngine;
        }

        public Project UseDatabaseEngine(DatabaseEngine engine, string serverName = null)
        {
            if (engine == DatabaseEngine.SQLServer2008)
            {
                Guard.That(() => serverName).IsNotNull();
                (engine as DatabaseEngine.SqlServerDatabaseEngine).ServerName = serverName;
                _databaseServer = serverName;
            }
            DatabaseEngine = engine;
            return this;
        }

        private string _databaseServer = string.Empty;
        public string DatabaseServer 
        { 
            get { return _databaseServer; } 
        }

    }
}