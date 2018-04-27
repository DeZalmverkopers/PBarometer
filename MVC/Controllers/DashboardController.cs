using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using Domain.Dashboards;

//Seppe

namespace MVC.Controllers
{
  public partial class DashboardController : Controller
  {
    // GET: Dashboard
    public virtual ActionResult Index()
    {
      return View();
    }

    public virtual ActionResult LaadGrafiekAanpassen()
    {
      List<SelectListItem> liX = new List<SelectListItem>
      {
        new SelectListItem { Text = "test1", Value = "test1" },
        new SelectListItem { Text = "test2", Value = "test2" },
        new SelectListItem { Text = "test3", Value = "test3" },
        new SelectListItem { Text = "test4", Value = "test4" },
        new SelectListItem { Text = "test5", Value = "test5" }
      };
      ViewBag.XWaarden = liX;

      List<SelectListItem> liY = new List<SelectListItem>
      {
        new SelectListItem { Text = "dummy1", Value = "test1" },
        new SelectListItem { Text = "dummy2", Value = "test2" },
        new SelectListItem { Text = "dummy3", Value = "test3" },
        new SelectListItem { Text = "dummy4", Value = "test4" },
        new SelectListItem { Text = "dummy5", Value = "test5" }
      };
      ViewBag.YWaarden = liY;

      List<SelectListItem> schaalopties = new List<SelectListItem>
      {
        new SelectListItem { Text = "dagen", Value = "dagen" },
        new SelectListItem { Text = "weken", Value = "weken" },
      };
      ViewBag.Schaalopties = schaalopties;

      return PartialView("~/Views/Shared/Dashboard/GrafiekAanpassen.cshtml", ViewBag);
    }

    public virtual ActionResult LaadLegePartialView()
    {
      return PartialView("~/Views/Shared/LegePartialView.cshtml");
    }
	
	public virtual ActionResult LaadGrafiekToevoegen()
    {
      List<SelectListItem> liGrafiekType = new List<SelectListItem>
      {
        new SelectListItem { Text = "Staafdiagram", Value = "staafdiagram" },
        new SelectListItem { Text = "Taartdiagram", Value = "taartdiagram" },
        new SelectListItem { Text = "Lijndiagram", Value = "lijndiagram" }
      };
      ViewBag.Grafiektypes = liGrafiekType;

      List<SelectListItem> liX = new List<SelectListItem>
      {
        new SelectListItem { Text = "test1", Value = "test1" },
        new SelectListItem { Text = "test2", Value = "test2" },
        new SelectListItem { Text = "test3", Value = "test3" },
        new SelectListItem { Text = "test4", Value = "test4" },
        new SelectListItem { Text = "test5", Value = "test5" }
      };
      ViewBag.XWaarden = liX;

      List<SelectListItem> liY = new List<SelectListItem>
      {
        new SelectListItem { Text = "dummy1", Value = "test1" },
        new SelectListItem { Text = "dummy2", Value = "test2" },
        new SelectListItem { Text = "dummy3", Value = "test3" },
        new SelectListItem { Text = "dummy4", Value = "test4" },
        new SelectListItem { Text = "dummy5", Value = "test5" }
      };
      ViewBag.YWaarden = liY;

      List<SelectListItem> schaalopties = new List<SelectListItem>
      {
        new SelectListItem { Text = "dagen", Value = "dagen" },
        new SelectListItem { Text = "weken", Value = "weken" },
      };
      ViewBag.Schaalopties = schaalopties;

      return PartialView("~/Views/Shared/Dashboard/GrafiekToevoegen.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop5()
    {
      return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml");
    }

    public virtual ActionResult LaadStaafdiagram()
    {
      return PartialView("~/Views/Shared/Grafieken/Staafdiagram.cshtml");
    }

    public virtual ActionResult LaadStaafdiagramMulti()
    {
      return PartialView("~/Views/Shared/Grafieken/StaafdiagramMulti.cshtml");
    }

    public virtual ActionResult LaadDonutdiagram()
    {
      return PartialView("~/Views/Shared/Grafieken/Donutdiagram.cshtml");
    }

    public virtual ActionResult LaadAlleGrafieken()
    {
      return PartialView("~/Views/Shared/Grafieken/Grafieken.cshtml");
    }


  }
}