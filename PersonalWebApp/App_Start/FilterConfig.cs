using PersonalWebApp.Attributes;
using System;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthorizeAttribute { });

            filters.Add(new LogFilterAttribute { });
        }
    }
}
