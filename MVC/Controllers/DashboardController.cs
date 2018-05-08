using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using Domain.Dashboards;
using Domain.Gemonitordeitems;

//Seppe

namespace MVC.Controllers
{
  public partial class DashboardController : Controller
  {
    // GET: Dashboard
    public virtual ActionResult Index()
    {
      GrafiekenManager grafiekenManager = new GrafiekenManager();
      List<Grafiek> grafieken = grafiekenManager.GetGrafieken(1, 1, true, true);

      ViewBag.Grafieken = grafieken;

      return View(ViewBag);
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

    public ActionResult VoegGrafiekToeEnUpdateDashboard(int deelplatformId, string titel, int periode, bool toonLegende, bool toonXAs, bool toonYAs, int keuze,
      string xTitel, string yTitel, bool xOnder, bool xOorsprongNul, bool yOorsprongNul, int dashboardId,
      string item1 = null, string waarde1 = "Vermeldingen",
      string item2 = null, string waarde2 = "Vermeldingen",
      string item3 = null, string waarde3 = "Vermeldingen",
      string item4 = null, string waarde4 = "Vermeldingen",
      string item5 = null, string waarde5 = "Vermeldingen")
    {
      GrafiekenManager grafiekenManager = new GrafiekenManager();
      GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
      List<GrafiekItem> grafiekItems = new List<GrafiekItem>();
      List<GemonitordItem> items = itemManager.GetGemonitordeItems(deelplatformId).ToList();
      List<GrafiekWaarde> waarden = new List<GrafiekWaarde>();

      List<string> itemStrings = new List<string>() { item1, item2, item3, item4, item5 };
      List<GrafiekWaarde> tijdelijkeWaarden = new List<GrafiekWaarde>() {
        (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
        (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
        (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
        (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
        (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true)
      };

      GrafiekKeuze grafiekKeuze = GrafiekKeuze.VergelijkingItemsOp1Moment;
      switch (keuze)
      {
        case 1: grafiekKeuze = GrafiekKeuze.KruisingTaart ;break;
        case 2: grafiekKeuze = GrafiekKeuze.KruisingBar; break;
        case 3: grafiekKeuze = GrafiekKeuze.EvolutieAantalVermeldingen1Item; break;
        case 4: grafiekKeuze = GrafiekKeuze.VergelijkingItemsDoorheenDeTijd; break;
        case 5: grafiekKeuze = GrafiekKeuze.VergelijkingItemsOp1Moment; break;
      }

      int teller = 0;
      foreach (string itemString in itemStrings)
      {
        if (itemString != null)
        {
          foreach (GemonitordItem item in items)
          {
            if (item.Naam.Equals(itemString))
            {
              grafiekItems.Add(new GrafiekItem { ItemId = item.GemonitordItemId });
              waarden.Add(tijdelijkeWaarden.ElementAt(teller));
            }
          }
        }
        teller++;
      }

      Grafiek grafiek = new Grafiek()
      {
        Titel = titel,
        Periode = periode,
        ToonLegende = toonLegende,
        ToonXAs = toonXAs,
        ToonYAs = toonYAs,
        Keuze = grafiekKeuze,
        XTitel = xTitel,
        YTitel = yTitel,
        Waarden = waarden,
        XOnder = xOnder,
        XOorsprongNul = xOorsprongNul,
        YOorsprongNul = yOorsprongNul,
        DashboardId = dashboardId,
        GrafiekItems = grafiekItems
      };

      grafiekenManager.AddGrafiek(grafiek);
      return RedirectToAction("Index");
    }

    public ActionResult UpdateGrafiekEnUpdateDashboard(int grafiekId, int deelplatformId, string titel, int periode, bool toonLegende, bool toonXAs, bool toonYAs, string type,
      string xTitel, string yTitel, bool xOnder, bool xOorsprongNul, bool yOorsprongNul, int dashboardId,
      string item1 = null, string waarde1 = "Vermeldingen",
      string item2 = null, string waarde2 = "Vermeldingen",
      string item3 = null, string waarde3 = "Vermeldingen",
      string item4 = null, string waarde4 = "Vermeldingen",
      string item5 = null, string waarde5 = "Vermeldingen")
    {
      GrafiekenManager grafiekenManager = new GrafiekenManager();
      GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
      List<GrafiekItem> grafiekItems = new List<GrafiekItem>();
      List<GemonitordItem> items = itemManager.GetGemonitordeItems(deelplatformId).ToList();
      List<GrafiekWaarde> waarden = new List<GrafiekWaarde>();

      List<string> itemStrings = new List<string>() { item1, item2, item3, item4, item5 };
      List<GrafiekWaarde> tijdelijkeWaarden = new List<GrafiekWaarde>() {
        (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
        (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
        (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
        (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
        (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true)
      };

      int teller = 0;
      foreach (string itemString in itemStrings)
      {
        if (itemString != null)
        {
          foreach (GemonitordItem item in items)
          {
            if (item.Naam.Equals(itemString))
            {
              grafiekItems.Add(new GrafiekItem { ItemId = item.GemonitordItemId });
              waarden.Add(tijdelijkeWaarden.ElementAt(teller));
            }
          }
        }
        teller++;
      }

      Grafiek grafiek = new Grafiek()
      {
        GrafiekId = grafiekId,
        Titel = titel,
        Periode = periode,
        ToonLegende = toonLegende,
        ToonXAs = toonXAs,
        ToonYAs = toonYAs,
        Type = (GrafiekType)Enum.Parse(typeof(GrafiekType), type, true),
        XTitel = xTitel,
        YTitel = yTitel,
        Waarden = waarden,
        XOnder = xOnder,
        XOorsprongNul = xOorsprongNul,
        YOorsprongNul = yOorsprongNul,
        DashboardId = dashboardId,
        GrafiekItems = grafiekItems
      };

      grafiekenManager.ChangeGrafiek(grafiek);
      return RedirectToAction("Index");
    }

    public ActionResult VerwijderGrafiekEnUpdateDashboard(int grafiekId)
    {
      GrafiekenManager grafiekenManager = new GrafiekenManager();
      Grafiek grafiek = new Grafiek()
      {
        GrafiekId = grafiekId
      };

      grafiekenManager.RemoveGrafiek(grafiek);
      return RedirectToAction("Index");
    }
  }
}