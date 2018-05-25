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
using Microsoft.ApplicationInsights.Extensibility;
using Domain.Deelplatformen;

namespace MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DisableApplicationInsightOnDebug();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ThreadStart tsTask = new ThreadStart(TaskLoop);
            Thread MyTask = new Thread(tsTask);
            MyTask.Start();
        }

        [Conditional("DEBUG")]
        private static void DisableApplicationInsightOnDebug()
        {
            TelemetryConfiguration.Active.DisableTelemetry = true;
        }

        static void TaskLoop()
        {
            while (true)
            {
                ScheduledTask2();
            }
        }


        static void ScheduledTask2()
        {
            IDataController dataController = new TextgainController();
            DeelplatformenManager deelplatformManager = new DeelplatformenManager();
            IEnumerable<Deelplatform> deelplatformen = deelplatformManager.GetDeelplatformen().ToList();
            if (deelplatformen != null && deelplatformen.Count() > 0)
            {
                var hoogsteFrequentie = deelplatformen.Max(d => d.DataOphaalFrequentie);
                for (int i = 0; i <= hoogsteFrequentie; i++)
                {
                    deelplatformen = deelplatformManager.GetDeelplatformen().ToList();
                    hoogsteFrequentie = deelplatformen.Max(d => d.DataOphaalFrequentie);
                    foreach (var deelplatform in deelplatformen)
                    {
                        if (i != 0 && (deelplatform.DataOphaalFrequentie % i == 0 || i % deelplatform.DataOphaalFrequentie == 0))
                        {
                            dataController.HaalBerichtenOp(deelplatform);
                        }
                    }
                    Thread.Sleep(60000);
                }
            }
        }
    }
}
