using System;
using System.Web;
using AopSample.Infrastructure.Bootstrapper;

namespace AopSample.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start(Object sender, EventArgs e)
        {
            ApplicationConfigurator.BuildContainer();
            ApplicationConfigurator.Start();
        }
        protected void Application_End(Object sender, EventArgs e)
        {
            ApplicationConfigurator.End();
        }
    }
}