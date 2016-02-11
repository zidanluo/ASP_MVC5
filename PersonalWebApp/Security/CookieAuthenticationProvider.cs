using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using log4net;
using System.Security.Claims;

namespace PersonalWebApp.Security
{
    public class CookieAuthenticationProvider : AuthenticationProvider
    {
        private ILog log = LogManager.GetLogger(typeof(CookieAuthenticationProvider));
        public override void SignIn(string username, string returnUrl = null)
        {
            ClaimsIdentity identity = new ClaimsIdentity(GetClaimsForUser(username), CookieAuthenticationDefaults.AuthenticationType);
            HttpContext.Current.GetOwinContext().Authentication.SignIn(identity);
            if (!string.IsNullOrEmpty(returnUrl))
                HttpContext.Current.Response.Redirect(returnUrl, false);
            else
                HttpContext.Current.Response.Redirect("/Home", false);
        }

        public override void SignOut()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
        }

        public override void Configure(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                LoginPath = new PathString("/Home/LogOn"),
                ExpireTimeSpan = TimeSpan.FromHours(1),
                SlidingExpiration = true,
                Provider = new Microsoft.Owin.Security.Cookies.CookieAuthenticationProvider
                {
                    OnApplyRedirect = x => x.Response.Redirect(x.RedirectUri),
                    OnResponseSignIn = x => log.Info("OnResponseSignIn" + x),
                    OnResponseSignedIn = x => log.Info("OnResponseSignedIn" + x),
                    OnResponseSignOut = x => log.Info("OnResponseSignOut" + x),
                }
            });
        }
    }
}