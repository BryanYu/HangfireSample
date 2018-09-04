using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using HangfireSample.Models;
using Microsoft.Owin.Security;

namespace HangfireSample.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var authentication = HttpContext.GetOwinContext().Authentication;
            if (authentication.User.Identity.IsAuthenticated)
            {
                authentication.SignOut();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserInfo user)
        {
            // 在這邊實作自訂的驗證機制
            if (user.Account == "admin" && user.Password == "123456")
            {
                var authentication = HttpContext.GetOwinContext().Authentication;
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, "admin"));
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                var identity = new ClaimsIdentity(claims, "HangfireLogin");

                authentication.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
                return Redirect("hangfire");
            }
            return this.View();
        }

    }
}