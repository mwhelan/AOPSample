using System;
using System.Collections.Generic;
using System.Linq;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using AopSample.Infrastructure.Data.Enumerations.DatabaseEngines;
using Seterlund.CodeGuard;

namespace AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration
{
    public class ConfigurationBuilder
    {
        private EnvironmentType _environmentType = EnvironmentType.Tdd;
        Project _defaultProject = new Project(ProjectType.Default);
        private List<Project> _projectOverrides = new List<Project>();

        public ConfigurationBuilder(EnvironmentType environmentType)
        {
            _environmentType = environmentType;
        }

        private ConfigurationBuilder(){}

        public ConfigurationBuilder DefaultTo(Action<Project> action)
        {
            action(_defaultProject);
            return this;
        }

        public ConfigurationBuilder OverrideFor(ProjectType projectType, Action<Project> action)
        {
            Project project = GetOverride(projectType);
            action(project);
            return this;
        }

        public static implicit operator Configuration(ConfigurationBuilder builder)
        {
            Guard.That(builder._defaultProject).IsNotNull();
            var engine = builder._defaultProject.DatabaseEngine;
            Guard.That(engine).IsNotEqual(DatabaseEngine.Null);
            if(engine == DatabaseEngine.SQLServer2008 && string.IsNullOrEmpty((engine as DatabaseEngine.SqlServerDatabaseEngine).ServerName))
                throw new ArgumentException("DatabaseServer");

            //Guard.That(builder._defaultProject.DatabaseServer).IsNotNullOrEmpty();
            return new Configuration(builder._environmentType, builder._defaultProject, builder._projectOverrides);
        }

        private Project GetOverride(ProjectType projectType)
        {
            var project = _projectOverrides.SingleOrDefault(x => x.ProjectType == projectType);
            if (project == null)
            {
                project = new Project(projectType);
                _projectOverrides.Add(project);
            }
            return project;
        }
    }
}