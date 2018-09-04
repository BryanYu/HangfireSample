using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using Hangfire;
using Hangfire.Dashboard;
using HangfireSample.App_Start;
using HangfireSample.Filter;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
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

            app.UseCookieAuthentication(
                new CookieAuthenticationOptions()
                {
                    AuthenticationType = "HangfireLogin",
                    LoginPath = new PathString("/Login/Index"),
                    LogoutPath = new PathString("/Login/Index")
                });
            
            app.UseHangfireDashboard(
                "/hangfire",
                new DashboardOptions() { Authorization = new[] { new MyAuthorizationFilter() },AppPath = VirtualPathUtility.ToAbsolute("~") });
            
            app.UseHangfireServer();
            JobConfig.Register();
        }
    }
}
