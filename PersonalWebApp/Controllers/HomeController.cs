using AopAlliance.Intercept;
using log4net;
using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ILog log = LogManager.GetLogger(typeof(HomeController));

        public ICommand Cmd { get; set; }
        public ActionResult Index()
        {
            Spring.Aop.Support.AttributeMatchMethodPointcutAdvisor s;
            Spring.Aop.Support.NameMatchMethodPointcutAdvisor n;
            log.Info("index");


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = Cmd.exec("abc");// "Your application description page.";
            log.Info("about");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = Cmd.exec("123");//"Your contact page.";
            log.Info("contact");
            return View();
        }
    }

    public interface ICommand
    {
        string exec(string context);
    }

    public class ServiceCommand : ICommand
    {
        public virtual string exec(string context)
        {
            ILog log = LogManager.GetLogger(typeof(ServiceCommand));
            log.Info(string.Format("Service implementation : [{0}]", context));
            return context;
        }
    }

    public class LogCallInterceptor : IMethodBeforeAdvice
    {
        public void Before(MethodInfo method, object[] args, object target)
        {
            ILog log = LogManager.GetLogger(typeof(LogCallInterceptor));
            log.Info(method.Name);
        }
    }

    public class LogAfter : Spring.Aop.IAfterReturningAdvice
    {
        
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            ILog log = LogManager.GetLogger(typeof(LogCallInterceptor));
            log.Info("After" + method.Name);
        }
    }
}