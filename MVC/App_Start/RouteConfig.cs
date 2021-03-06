﻿using System.Web.Mvc;
using System.Web.Routing;

namespace MVC
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          name: "Default",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
          constraints: new { controller = "Home|Account|SuperAdmin|api|Profiel" }
      );

      routes.MapRoute(
         name: "Deelplatform",
         url: "{deelplatform}/{controller}/{action}/{id}",
         defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

     );

    }
  }
}
