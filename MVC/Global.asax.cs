using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using MVC.App_Start;
using System.Threading;
using MVC.Controllers.Api;
using BL;
using System.Diagnostics;

namespace MVC
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
            ThreadStart tsTask = new ThreadStart(TaskLoop);
            Thread MyTask = new Thread(tsTask);
            MyTask.Start();
        }

        static void TaskLoop()
        {
            while (true)
            {
                ScheduledTask();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }

        static void ScheduledTask()
        {
            Debug.WriteLine("Ophalen Data - Start");
            //TextgainController textgainController = new TextgainController();
            //DeelplatformenManager deelplatformManager = new DeelplatformenManager();
            //int id = deelplatformManager.GetDeelplatformByName("Politieke Barometer").DeelplatformId;
            //textgainController.HaalBerichtenOp(deelplatformManager.GetDeelplatform(id));
            //Debug.WriteLine("Ophalen Data - Done");
        }
    }
}
