using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration;
using AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Types;
using AopSample.Infrastructure.Data.Enumerations.DatabaseEngines;

namespace AopSample.Infrastructure.Bootstrapper.Installers
{
    public class ConfigurationFacility : AbstractFacility
    {
        Configuration _configuration;

        protected override void Init()
        {
            RegisterStandardConfiguration();
            Kernel.Register(Component.For<IConfiguration>().Instance(_configuration));
        }

        private void RegisterStandardConfiguration()
        {
            _configuration = new ConfigurationBuilder(EnvironmentType.Tdd)
                .DefaultTo(project => project
                               .UseDatabaseEngine(DatabaseEngine.SqlCE))
                .OverrideFor(ProjectType.AcceptanceTests,
                             project => project.UseDatabaseEngine(DatabaseEngine.SQLiteInMemory))
                .OverrideFor(ProjectType.IntegrationTests,
                             project => project.UseDatabaseEngine(DatabaseEngine.SQLiteFileSystem));
        }
    }
}
