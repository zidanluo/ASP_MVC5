using AopAlliance.Intercept;
using log4net;
using PersonalWebApp.Data;
using PersonalWebApp.Models;
using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
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

        //public ICommand Cmd { get; set; }
        public ActionResult Index()
        {
            //Spring.Aop.Support.AttributeMatchMethodPointcutAdvisor s;
            //Spring.Aop.Support.NameMatchMethodPointcutAdvisor n;
            return View();
        }

        public ActionResult About()
        {
            // "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            //"Your contact page.";
            return View();
        }

        public ActionResult LogOn(string returnUrl)
        {
            return View(new LogOnModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                using (WebAppDbContext ctx = new WebAppDbContext())
                {
                    try
                    {
                        //SQLiteConnection a = new SQLiteConnection(@"data source=|DataDirectory|WebApp.db");
                        //a.Open();
                        //a.ChangePassword("123");
                        //log.Info(a.State);
                        //a.Close();
                        ctx.Database.Log = log.Info;
                        var u = ctx.Users.Where(x => x.UserId == model.Username);
                        if (u.Count() > 0)
                            model.Password = u.FirstOrDefault().Email;
                        else
                            ModelState.AddModelError("", "No Record Found");
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message, ex);
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                //ModelState.AddModelError("", "xxx");
            }
            return View(model);
        }
    }


    //public interface ICommand
    //{
    //    string exec(string context);
    //}

    //public class ServiceCommand : ICommand
    //{
    //    public virtual string exec(string context)
    //    {
    //        ILog log = LogManager.GetLogger(typeof(ServiceCommand));
    //        log.Info(string.Format("Service implementation : [{0}]", context));
    //        return context;
    //    }
    //}

    //public class LogCallInterceptor : IMethodBeforeAdvice
    //{
    //    public void Before(MethodInfo method, object[] args, object target)
    //    {
    //        ILog log = LogManager.GetLogger(typeof(LogCallInterceptor));
    //        log.Info(method.Name);
    //    }
    //}

    //public class LogAfter : Spring.Aop.IAfterReturningAdvice
    //{

    //    public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
    //    {
    //        ILog log = LogManager.GetLogger(typeof(LogCallInterceptor));
    //        log.Info("After" + method.Name);
    //    }
    //}
}