using Hangfire.Dashboard;
using Microsoft.Owin;

namespace HangfireSample.Filter
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var owinContext = new OwinContext(context.GetOwinEnvironment());
            return owinContext.Authentication.User.Identity.IsAuthenticated;
        }
    }
}