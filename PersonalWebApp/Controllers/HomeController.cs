using AopAlliance.Intercept;
using log4net;
using Microsoft.Practices.Unity;
using PersonalWebApp.Data;
using PersonalWebApp.Models;
using PersonalWebApp.Security;
using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ILog log = LogManager.GetLogger(typeof(HomeController));

        [Dependency]
        public IMembershipService MemberService { get; set; }

        [Dependency]
        public ISecurityValidator SecurityValidator { get; set; }

        [Dependency]
        public IAuthenticationProvider AuthenticationProvider { get; set; }
        public ActionResult Index()
        {
            //Spring.Aop.Support.AttributeMatchMethodPointcutAdvisor s;
            //Spring.Aop.Support.NameMatchMethodPointcutAdvisor n;
            return View();
        }

        //public ActionResult About()
        //{
        //    // "Your application description page.";
        //    return View();
        //}

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Resume()
        {
            HttpCookie c = Request.Cookies.Get("ResumeAccess");
            if (c != null && "Y" == c.Value)
                return View("ViewResume");
            return View();
        }

        [HttpPost]
        public ActionResult ViewResume(ResumeModel model)
        {
            if (ModelState.IsValid)
            {
                if (SecurityValidator.Validate(model.AccessCode))
                {
                    HttpCookie c = new HttpCookie("ResumeAccess", "Y");
                    int days_access = 7;//TODO :config this in web.config later
                    c.Expires = DateTime.Now.AddDays(days_access);
                    Response.SetCookie(c);
                    return View();
                }
                else
                    ModelState.AddModelError("", "访问码错误");
            }
            return View("Resume", model);

        }

        public ActionResult LogOff()
        {
            AuthenticationProvider.SignOut();
            return RedirectToAction("LogOn", "Home");
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
                ValidateResult result = MemberService.ValidateUser(model.Username, model.Password);
                switch (result)
                {
                    case ValidateResult.Success:
                        AuthenticationProvider.SignIn(model.Username, model.ReturnUrl);
                        return new EmptyResult();
                    case ValidateResult.Failure:
                        ModelState.AddModelError("", "Login failed, username or password is wrong");
                        break;
                }
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