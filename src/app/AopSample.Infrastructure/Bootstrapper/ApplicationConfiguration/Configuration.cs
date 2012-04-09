using System.Collections.Generic;
using System.Linq;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using AopSample.Infrastructure.Data.Enumerations.DatabaseEngines;
using Shared.Core.Collections;

namespace AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration
{
    public class Configuration : IConfiguration
    {
        public EnvironmentType EnvironmentType { get; protected set; }
        IDictionary<ProjectType, Project> _configurations;// = new Dictionary<ProjectType, Project>();

        public Configuration(EnvironmentType environmentType, Project defaultProject, List<Project> projectOverrides)
        {
            EnvironmentType = environmentType;
            _configurations = projectOverrides
                .ToDictionary(x => x.ProjectType)
                .WithDefaultValue(defaultProject);
        }

        public string DatabaseServerFor(ProjectType projectType)
        {
            return !string.IsNullOrEmpty(_configurations[projectType].DatabaseServer) 
                ? _configurations[projectType].DatabaseServer 
                : _configurations[ProjectType.Default].DatabaseServer;
        }

        public DatabaseEngine DatabaseEngineFor(ProjectType projectType)
        {
            return _configurations[projectType].DatabaseEngine != DatabaseEngine.Null 
                ? _configurations[projectType].DatabaseEngine 
                : _configurations[ProjectType.Default].DatabaseEngine;
        }

        public string ConnectionStringFor(ProjectType projectType)
        {
            return DatabaseEngineFor(projectType).ConnectionStringFor(projectType);
        }
    }
}