using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;
using Microsoft.Practices.Unity;
using PersonalWebApp.Security;
using Spring.Web.Providers;

namespace PersonalWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            XmlConfigurator.Configure();
            RegisgerDependencyResolver();
        }

        private static void RegisgerDependencyResolver()
        {
            var container = new UnityContainer().AddExtension(new UnityDecoratorContainerExtension());

            container.RegisterType<IMembershipService, EFMembershipService>();
            container.RegisterType<IAuthenticationProvider, CookieAuthenticationProvider>();
            container.RegisterType<IRoleProvider, EFRoleProvider>();
            container.RegisterType<ISecurityValidator, ValidPeriodValidator>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
