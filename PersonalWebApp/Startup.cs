using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Owin;
using Microsoft.Owin;
using PersonalWebApp;
using PersonalWebApp.Security;

[assembly: OwinStartup(typeof(Startup))]
namespace PersonalWebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DependencyResolver.Current.GetService<IAuthenticationProvider>().Configure(app);
        }
    }
}