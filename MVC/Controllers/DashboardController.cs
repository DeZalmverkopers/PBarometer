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

      return PartialView(MVC.Shared.Views.Dashboard.GrafiekAanpassen, ViewBag);
    }

    public virtual ActionResult LaadLegePartialView()
    {
      return PartialView(MVC.Shared.Views.LegePartialView);
    }
	
	//TODO
	public virtual ActionResult LaadGeavanceerdeInstellingen()
    {
      return PartialView("GeavanceerdeInstellingen");
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

      return PartialView(MVC.Shared.Views.Dashboard.GrafiekToevoegen, ViewBag);
    }

    public virtual ActionResult LaadTop5()
    {
      return PartialView(MVC.Shared.Views.GetalEnOverzicht.Top5);
    }

    public virtual ActionResult LaadStaafdiagram()
    {
      return PartialView(MVC.Shared.Views.Grafieken.Staafdiagram);
    }

    public virtual ActionResult LaadStaafdiagramMulti()
    {
      return PartialView(MVC.Shared.Views.Grafieken.StaafdiagramMulti);
    }

    public virtual ActionResult LaadDonutdiagram()
    {
      return PartialView(MVC.Shared.Views.Grafieken.Donutdiagram);
    }

    public virtual ActionResult LaadAlleGrafieken()
    {
      return PartialView(MVC.Shared.Views.Grafieken.Grafieken);
    }


  }
}