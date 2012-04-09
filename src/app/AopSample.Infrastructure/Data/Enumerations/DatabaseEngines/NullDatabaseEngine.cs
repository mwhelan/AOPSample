using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using FluentNHibernate.Cfg.Db;

namespace AopSample.Infrastructure.Data.Enumerations.DatabaseEngines
{
    public abstract partial class DatabaseEngine
    {
        private class NullDatabaseEngine : DatabaseEngine
        {
            public NullDatabaseEngine()
                : base(1, "Null")
            {
            }

            public override IPersistenceConfigurer PersistenceConfigurer(ProjectType projectType)
            {
                return null;
            }

            public override string ConnectionStringFor(ProjectType projectType)
            {
                return null;
            }
        }
    }
}