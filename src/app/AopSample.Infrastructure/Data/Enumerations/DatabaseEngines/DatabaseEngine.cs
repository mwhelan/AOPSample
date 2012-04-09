using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using FluentNHibernate.Cfg.Db;
using Shared.Core.Collections;

namespace AopSample.Infrastructure.Data.Enumerations.DatabaseEngines
{
    //public enum DatabaseEngine
    //{
    //    Null,
    //    SQLServer2008,
    //    SQLiteFileSystem,
    //    SQLiteInMemory,
    //    SQLServer2008FromWebConfig,
    //    SqlCE
    //}

    public abstract partial class DatabaseEngine : Enumeration
    {
        public static readonly DatabaseEngine Null = new NullDatabaseEngine();
        public static readonly DatabaseEngine SQLServer2008 = new SqlServerDatabaseEngine();
        public static readonly DatabaseEngine SQLiteFileSystem = new SQLiteFileSystemDatabaseEngine();
        public static readonly DatabaseEngine SQLiteInMemory = new SQLiteInMemoryDatabaseEngine();
        public static readonly DatabaseEngine SQLServer2008FromWebConfig = new SQLServerFromConfigDatabaseEngine();
        public static readonly DatabaseEngine SqlCE = new SqlCEDatabaseEngine();

        private DatabaseEngine() { }

        private DatabaseEngine(int value, string displayName) : base(value, displayName) { }

        public abstract IPersistenceConfigurer PersistenceConfigurer(ProjectType projectType);
        public abstract string ConnectionStringFor(ProjectType projectType);
    }
}