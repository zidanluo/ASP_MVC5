using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebApp.Attributes
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private ILog log = LogManager.GetLogger(typeof(LogFilterAttribute));
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            log.Info(string.Format("{0}({1}), Path: {2}", filterContext.RequestContext.HttpContext.Request.UserHostAddress, filterContext.RequestContext.HttpContext.Request.UserHostName, filterContext.RequestContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath));

        }
    }
}