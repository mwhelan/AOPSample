using System;
using System.Diagnostics;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;

namespace AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Extensions
{
    public static class ProcessExtensions
    {
        const string SqLiteFileName = "SqLiteFileName";

        public static void SetProjectType(this ProcessStartInfo startInfo, ProjectType projectType)
        {
            startInfo.EnvironmentVariables.Add(typeof(ProjectType).Name, projectType.ToString());
        }

        public static void SetSqLiteFileName(this ProcessStartInfo startInfo, string fileName)
        {
            startInfo.EnvironmentVariables.Add(SqLiteFileName, fileName);
        }

        public static ProjectType? GetProjectType(this Process process)
        {
            var projectTypeValue = process.StartInfo.EnvironmentVariables[typeof(ProjectType).Name];

            ProjectType possibleProjectType;

            var enumParsed = Enum.TryParse(projectTypeValue, out possibleProjectType);

            return enumParsed ? (ProjectType?)possibleProjectType : null;
        }

        public static string GetSqLiteFileName(this Process process)
        {
            var sqLiteFileName = process.StartInfo.EnvironmentVariables[SqLiteFileName];

            return sqLiteFileName;
        }
    }
}
