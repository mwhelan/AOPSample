using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AopSample.Infrastructure.Security;
using FluentSecurity;

namespace AopSample.Infrastructure.Bootstrapper.StartupTasks
{
    public class SecurityBootstrapper : IRunTaskAtStartup
    {
        public void Execute()
        {
            SecurityConfigurator.Configure(configuration =>
            {
                configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
                var assembly = Assembly.Load(ConfigSettings.WebAssemblyName);
                configuration.ForAllControllersInAssembly(assembly).DenyAnonymousAccess();
            });

            GlobalFilters.Filters.Add(new CustomHandleSecurityAttribute(), 0);
            string whatDoIHave = SecurityConfiguration.Current.WhatDoIHave();
        }
    }
}