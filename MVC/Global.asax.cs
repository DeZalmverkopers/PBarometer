﻿using System.Web;
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
using System.Collections.Generic;
using Domain.Deelplatformen;
using System.Linq;

namespace MVC
{
  public class MvcApplication : HttpApplication
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
      ScheduledTask1();
      while (true)
      {
        ScheduledTask2();
      }
    }

    static void ScheduledTask1()
    {
      Debug.WriteLine("Ophalen Data 1 - Start");
      DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
      int id = deelplatformenManager.GetDeelplatformByName("Politieke Barometer").DeelplatformId;
      GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
      gemonitordeItemsManager.AddOrganisatie("Open VLD", id, new List<string>() { "Alexander De Croo", "Gwendolyn Rutten", "Maggie De Block" });
      gemonitordeItemsManager.AddOrganisatie("Groen", id, new List<string>() { "Kristof Calvo", "Meyrem Almaci", "Wouter Van Besien" });
      gemonitordeItemsManager.AddOrganisatie("SPA", id, new List<string>() { "Caroline Gennez", "John Crombez", "Bruno Tobback" });
      gemonitordeItemsManager.AddOrganisatie("Vlaams Belang", id, new List<string>() { "Filip Dewinter", "Tom Van Grieken", "Gerolf Annemans" });

      gemonitordeItemsManager.AddThema("Migratie", new List<string>() { "buitenland", "vluchteling", "immigratie", "migratie" }, id);
      gemonitordeItemsManager.AddThema("Fiscaliteit", new List<string>() { "belastingen", "tax", "btw", "sociale zekerheid" }, id);
      gemonitordeItemsManager.AddThema("Milieu", new List<string>() { "kernenergie", "zonnenergie", "steenkool", "luchtvervuiling", "windenergie" }, id);


      Debug.WriteLine("Ophalen Data 2 - Done");
    }

    static void ScheduledTask2()
    {
      Debug.WriteLine("Invoegen Textgain 1 - Start");
      TextgainController textgainController = new TextgainController();
      DeelplatformenManager deelplatformManager = new DeelplatformenManager();
      IEnumerable<Deelplatform> deelplatformen = deelplatformManager.GetDeelplatformen();

      Debug.WriteLine("Invoegen Textgain 2 - Done");
    }
  }
}
