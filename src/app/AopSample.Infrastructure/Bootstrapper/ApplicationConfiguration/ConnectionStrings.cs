using System;
using System.IO;
using System.Linq;

namespace AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration
{
    public static class ConnectionStrings
    {
        public const string WebConfigConnectionStringNameFormat = "{0}ConnectionString";
        public const string SqlServerConnectionStringFormat = "server={0};database=EpisodeGuide_{1};Trusted_Connection=True";
        //const string SqlCEConnectionStringFormat = "EpisodeGuide_{0}.sdf";

        //public static string SqlServerFromConfig(ProjectType projectType)
        //{
        //    var connectionStringName = string.Format(WebConfigConnectionStringNameFormat, projectType);

        //    var connectionStringElement = ConfigurationManager.ConnectionStrings[connectionStringName];

        //    if (connectionStringElement == null)
        //    {
        //        const string messageFormat = "The '{0}' project has been configured to use a connection string in Web.Config named '{1}', but there is no such connection string.";
        //        string message = string.Format(
        //            messageFormat,
        //            projectType,
        //            connectionStringName);

        //        throw new ConfigurationErrorsException(message);
        //    }

        //    return connectionStringElement.ConnectionString;
        //}

        //public static string SqlServer(string serverName, ProjectType projectType)
        //{
        //    return string.Format(SqlServerConnectionStringFormat, serverName, projectType);
        //}

        //public static string SqlCE(ProjectType projectType)
        //{
        //    // All .sdf files stored in web application App_Data folder, for the moment.
        //    string web = string.Empty;
        //    if(projectType == ProjectType.Application)
        //        web = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
        //    if(string.IsNullOrEmpty(web))
        //        web = GetProjectPath(ConfigSettings.WebAssemblyName) + @"\App_Data";
        //    string dbName = string.Format(SqlCEConnectionStringFormat, projectType);
        //    string fullDbPath = string.Format("{0}\\{1}", web, dbName);

        //    string connectionString = string.Format("Data Source={0};Persist Security Info=False;", fullDbPath);
        //    return connectionString;
        //}

        public static string GetProjectPath(string projectFolderName)
        {
            string path = Path.GetDirectoryName(Environment.CurrentDirectory);
            var directory = new DirectoryInfo(path);

            while (directory.GetFiles("*.sln").Length == 0)
            {
                directory = directory.Parent;
            }

            directory = (directory.GetDirectories("*", SearchOption.AllDirectories)
                .Where(folder => folder.Name == projectFolderName))
                .FirstOrDefault();

            if (directory == null)
                throw new DirectoryNotFoundException();

            return directory.FullName;
        }
    }
}
