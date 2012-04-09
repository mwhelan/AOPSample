using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using AopSample.Infrastructure.Data.Enumerations.DatabaseEngines;

namespace AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration
{
    public interface IConfiguration
    {
        EnvironmentType EnvironmentType { get; }
        string DatabaseServerFor(ProjectType projectType);
        DatabaseEngine DatabaseEngineFor(ProjectType application);
        string ConnectionStringFor(ProjectType projectType);
    }
}