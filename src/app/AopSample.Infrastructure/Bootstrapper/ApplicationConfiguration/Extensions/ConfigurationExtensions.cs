//using System;
//using System.IO;

//using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
//using AopSample.Infrastructure.Data;
//using AopSample.Infrastructure.Data.Enumerations.DatabaseEngines;
//using FluentNHibernate.Cfg.Db;
//using Environment = NHibernate.Cfg.Environment;

//namespace AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Extensions
//{
//    public static class ConfigurationExtensions
//    {
//        public static IPersistenceConfigurer GetPersistenceConfigurer(this IConfiguration configuration, ProjectType? projectType)
//        {
//            var DatabaseEngine = configuration.DatabaseEngineFor(projectType.Value);
//            var databaseServer = configuration.DatabaseServerFor(projectType.Value);
//            var connectionString = configuration.ConnectionStringFor(projectType.Value);

//            switch (DatabaseEngine)
//            {
//                //case DatabaseEngine.SqlCE:
//                //    return MsSqlCeConfiguration
//                //        .Standard
//                //        .ConnectionString(connection => connection.Is(connectionString))
//                //        .ShowSql()
//                //        .FormatSql()
//                //        //.Dialect("NHibernate.Dialect.MsSqlCe40Dialect")
//                //        .Dialect<MsSqlCe40DialectWithVariableLimit>()
//                //        .AdoNetBatchSize(100);

//                //case DatabaseEngine.SQLServer2008FromWebConfig:

//                //    return MsSqlConfiguration
//                //        .MsSql2008
//                //        .ConnectionString(connectionString)
//                //        .ShowSql()
//                //        .FormatSql()
//                //        .AdoNetBatchSize(100);

//                //case DatabaseEngine.SQLServer2008:

//                //    if (databaseServer == null)
//                //    {
//                //        return MsSqlConfiguration
//                //            .MsSql2008
//                //            .ShowSql()
//                //            .FormatSql()
//                //            .AdoNetBatchSize(100);
//                //    }

//                //    return MsSqlConfiguration
//                //        .MsSql2008
//                //        .ConnectionString(connectionString)
//                //        .ShowSql()
//                //        .FormatSql()
//                //        .AdoNetBatchSize(100);

//                //case DatabaseEngine.SQLiteFileSystem:

//                //    //DeletePreviousDbFiles();
//                //    //var dbFile = GetDbFileName();

//                //    return SQLiteConfiguration
//                //        .Standard.UsingFile("Bert")
//                //        .ShowSql()
//                //        .FormatSql()
//                //        .AdoNetBatchSize(100);

//                //case DatabaseEngine.SQLiteInMemory:

//                //    var sqLiteConfiguration = SQLiteConfiguration
//                //        .Standard.InMemory()
//                //        .Raw(Environment.ConnectionProvider, typeof(SqliteCachedConnectionProvider).AssemblyQualifiedName)
//                //        .ShowSql()
//                //        .FormatSql()
//                //        .AdoNetBatchSize(100);

//                //    return sqLiteConfiguration;

//                //default:

//                //    throw new NotImplementedException();
//            }
//        }

//        private static string GetDbFileName()
//        {
//            var path = Path.GetFullPath(Path.GetRandomFileName() + ".Test.db");
//            return !File.Exists(path) ? path : GetDbFileName();
//        }

//        private static void DeletePreviousDbFiles()
//        {
//            var files = Directory.GetFiles(".", "*.Test.db*");
//            foreach (var file in files)
//            {
//                File.Delete(file);
//            }
//        }
//    }
//}
