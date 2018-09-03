using System;
using System.Configuration;
using System.Threading.Tasks;
using Hangfire;
using HangfireSample.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HangfireSample.Startup))]

namespace HangfireSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HangfireConnection"].ConnectionString;
            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            JobConfig.Register();
        }
    }
}
