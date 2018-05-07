using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using Domain.Dashboards;
using Domain.Deelplatformen;
using Domain.Gemonitordeitems;
using MVC.Controllers.Api;

//Seppe

namespace MVC.Controllers
{
  public partial class DashboardController : Controller
  {
    GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
    List<SelectListItem> selects;

    HomeController homeController = new HomeController();

    // GET: Dashboard
    public virtual ActionResult Index()
    {
      homeController.GetData();


      GetData();

      //GrafiekenManager grafiekenManager = new GrafiekenManager();
      //List<Grafiek> grafieken = grafiekenManager.GetGrafieken(1, 1, true, true);

      //ViewBag.Grafieken = grafieken;

      return View();
    }

    public virtual ActionResult LaadElementAanpassen()
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
        //new SelectListItem { Text = "dummy4", Value = "test4" },
        //new SelectListItem { Text = "dummy5", Value = "test5" }
      };
      ViewBag.YWaarden = liY;

      List<SelectListItem> schaalopties = new List<SelectListItem>
      {
        new SelectListItem { Text = "dagen", Value = "dagen" },
        new SelectListItem { Text = "weken", Value = "weken" },
      };
      ViewBag.Schaalopties = schaalopties;

      return PartialView("~/Views/Shared/Dashboard/ElementAanpassen.cshtml", ViewBag);
    }

    public virtual ActionResult LaadLegePartialView()
    {
      return PartialView("~/Views/Shared/LegePartialView.cshtml");
    }

    public virtual ActionResult LaadElementToevoegen()
    {
      List<GemonitordItem> items;


      items = itemManager.GetPersonen(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.Personen = selects;



      //items = itemManager.GetThemas(1).ToList();
      //selects = new List<SelectListItem>();
      //foreach (var item in items)
      //{
      //  selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      //}

      //ViewBag.Themas = selects;


      items = itemManager.GetOrganisaties(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.Organisaties = selects;

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

      return PartialView("~/Views/Shared/Dashboard/ElementToevoegen.cshtml", ViewBag);

    }

    public virtual ActionResult LaadGetal(string item)
    {
      ViewBag.Titel = item;

      return PartialView("~/Views/Shared/GetalEnOverzicht/Getal.cshtml", ViewBag);
    }

    public virtual ActionResult LaadGetalTrend(string item)
    {
      ViewBag.Titel = item;


      return PartialView("~/Views/Shared/GetalEnOverzicht/GetalTrend.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop5(string item)
    {
      ViewBag.Titel = item;

      return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop10(string item)
    {
      ViewBag.Titel = item;

      return PartialView("~/Views/Shared/GetalEnOverzicht/Top10.cshtml", ViewBag);
    }



    //public virtual ActionResult LaadGetal(GemonitordItem item)
    //{
    //  ViewBag.Item = item;

    //  return PartialView("~/Views/Shared/GetalEnOverzicht/Getal.cshtml", ViewBag);
    //}

    //public virtual ActionResult LaadGetalTrend(GemonitordItem item)
    //{
    //  ViewBag.Item = item;

    //  return PartialView("~/Views/Shared/GetalEnOverzicht/GetalTrend.cshtml", ViewBag);
    //}

    //public virtual ActionResult LaadTop5(GemonitordItem item)
    //{
    //  ViewBag.Item = item;

    //  return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml", ViewBag);
    //}

    //public virtual ActionResult LaadTop10(GemonitordItem item)
    //{
    //  ViewBag.Item = item;

    //  return PartialView("~/Views/Shared/GetalEnOverzicht/Top10.cshtml", ViewBag);
    //}




    public void LaadItem(List<GemonitordItem> items)
    {
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }
    }

    public virtual ActionResult LaadOrganisaties1Item()
    {
      LaadItem(itemManager.GetOrganisaties(1).ToList());

      ViewBag.Organisaties = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties1Item.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties2Items()
    {
      LaadItem(itemManager.GetOrganisaties(1).ToList());

      ViewBag.Organisaties = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties2Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties3Items()
    {
      LaadItem(itemManager.GetOrganisaties(1).ToList());

      ViewBag.Organisaties = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties3Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties4Items()
    {
      LaadItem(itemManager.GetOrganisaties(1).ToList());

      ViewBag.Organisaties = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties4Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties5Items()
    {
      LaadItem(itemManager.GetOrganisaties(1).ToList());

      ViewBag.Organisaties = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties5Items.cshtml", ViewBag);
    }


    public virtual ActionResult LaadPersonen1Item()
    {

      LaadItem(itemManager.GetPersonen(1).ToList());

      ViewBag.Personen = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen1Item.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen2Items()
    {
      LaadItem(itemManager.GetPersonen(1).ToList());

      ViewBag.Personen = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen2Items.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen3Items()
    {
      LaadItem(itemManager.GetPersonen(1).ToList());

      ViewBag.Personen = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen3Items.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen4Items()
    {
      LaadItem(itemManager.GetPersonen(1).ToList());

      ViewBag.Personen = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen4Items.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen5Items()
    {
      LaadItem(itemManager.GetPersonen(1).ToList());

      ViewBag.Personen = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen5Items.cshtml", ViewBag);

    }

    //public virtual ActionResult LaadThemas1Item()
    //{
    //  LaadItem(itemManager.GetThemas(1).ToList());

    //  ViewBag.Themas = selects;

    //  return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas1Item.cshtml", ViewBag);
    //}

    //public virtual ActionResult LaadThemas2Items()
    //{
    //  LaadItem(itemManager.GetThemas(1).ToList());

    //  ViewBag.Themas = selects;

    //  return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas2Items.cshtml", ViewBag);
    //}

    //public virtual ActionResult LaadThemas3Items()
    //{
    //  LaadItem(itemManager.GetThemas(1).ToList());

    //  ViewBag.Themas = selects;

    //  return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas3Items.cshtml", ViewBag);
    //}

    //public virtual ActionResult LaadThemas4Items()
    //{
    //  LaadItem(itemManager.GetThemas(1).ToList());

    //  ViewBag.Themas = selects;

    //  return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas4Items.cshtml", ViewBag);
    //}

    //public virtual ActionResult LaadThemas5Items()
    //{
    //  LaadItem(itemManager.GetThemas(1).ToList());

    //  ViewBag.Themas = selects;

    //  return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas5Items.cshtml", ViewBag);
    //}



    public virtual ActionResult LaadStatistiekenKeuze()
    {
      return PartialView("~/Views/Shared/Dashboard/Statistieken/StatistiekenKeuze.cshtml");
    }

    public virtual ActionResult LaadGetalKeuze()
    {
      return PartialView("~/Views/Shared/Dashboard/Statistieken/GetalKeuze.cshtml");
    }

    public virtual ActionResult LaadGetalTrendKeuze()
    {
      return PartialView("~/Views/Shared/Dashboard/Statistieken/GetalTrendKeuze.cshtml");
    }

    public virtual ActionResult LaadTop5Keuze()
    {
      return PartialView("~/Views/Shared/Dashboard/Statistieken/Top5Keuze.cshtml");
    }

    public virtual ActionResult LaadTop10Keuze()
    {
      return PartialView("~/Views/Shared/Dashboard/Statistieken/Top10Keuze.cshtml");
    }


    public virtual ActionResult LaadGrafiekenKeuze()
    {
      return PartialView("~/Views/Shared/Dashboard/Grafieken/GrafiekenKeuze.cshtml");
    }

    public virtual ActionResult LaadAantalTweets()
    {
      return PartialView("~/Views/Shared/Dashboard/Grafieken/AantalTweets.cshtml");
    }

    public virtual ActionResult LaadItemsKruisen()
    {
      return PartialView("~/Views/Shared/Dashboard/Grafieken/ItemsKruisen.cshtml");
    }

    public virtual ActionResult LaadVergelijkenDoorheenDeTijd()
    {
      return PartialView("~/Views/Shared/Dashboard/Grafieken/VergelijkenDoorheenDeTijd.cshtml");
    }

    public virtual ActionResult LaadVergelijkenOpMoment()
    {
      return PartialView("~/Views/Shared/Dashboard/Grafieken/VergelijkenOpMoment.cshtml");
    }


    public virtual ActionResult LaadStaafdiagram(string grafiektitel)
    {

      ViewBag.labels = new string[] { "item1", "item2", "item3", "item4", "item5" };
      ViewBag.data = new double[] { 50, 40, 70, 30, 60 };
      ViewBag.grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Staafdiagram.cshtml", ViewBag);

    }


    //public virtual ActionResult LaadStaafdiagram()
    //{
    //  return PartialView("~/Views/Shared/Grafieken/Staafdiagram.cshtml");
    //}



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




    public ActionResult VoegGrafiekToeEnUpdateDashboard(string titel, int periode, bool toonLegende, bool toonXAs, bool toonYAs, GrafiekType type,
      string xTitel, string yTitel, bool xOnder, bool xOorsprongNul, bool yOorsprongNul, int dashboardId,
      GemonitordItem item1 = null, GrafiekWaarde waarde1 = GrafiekWaarde.Vermeldingen,
      GemonitordItem item2 = null, GrafiekWaarde waarde2 = GrafiekWaarde.Vermeldingen,
      GemonitordItem item3 = null, GrafiekWaarde waarde3 = GrafiekWaarde.Vermeldingen,
      GemonitordItem item4 = null, GrafiekWaarde waarde4 = GrafiekWaarde.Vermeldingen,
      GemonitordItem item5 = null, GrafiekWaarde waarde5 = GrafiekWaarde.Vermeldingen)
    {
      GrafiekenManager grafiekenManager = new GrafiekenManager();
      List<GrafiekItem> grafiekItems = new List<GrafiekItem>();
      List<GrafiekWaarde> waarden = new List<GrafiekWaarde>();
      Grafiek grafiek;

      if (item1 != null)
      {
        grafiekItems.Add(new GrafiekItem() { ItemId = item1.GemonitordItemId });
        waarden.Add(waarde1);

        if (item2 != null)
        {
          grafiekItems.Add(new GrafiekItem() { ItemId = item2.GemonitordItemId });
          waarden.Add(waarde2);
        }

        if (item3 != null)
        {
          grafiekItems.Add(new GrafiekItem() { ItemId = item3.GemonitordItemId });
          waarden.Add(waarde3);
        }

        if (item4 != null)
        {
          grafiekItems.Add(new GrafiekItem() { ItemId = item4.GemonitordItemId });
          waarden.Add(waarde4);
        }

        if (item5 != null)
        {
          grafiekItems.Add(new GrafiekItem() { ItemId = item5.GemonitordItemId });
          waarden.Add(waarde5);
        }

        grafiek = new Grafiek()
        {
          Titel = titel,
          Periode = periode,
          ToonLegende = toonLegende,
          ToonXAs = toonXAs,
          ToonYAs = toonYAs,
          Type = type,
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
      }

      return RedirectToAction("Index");
    }

    public ActionResult UpdateGrafiekEnUpdateDashboard(int id, string titel, int periode, bool toonLegende, bool toonXAs, bool toonYAs, GrafiekType type,
      string xTitel, string yTitel, bool xOnder, bool xOorsprongNul, bool yOorsprongNul, int dashboardId,
      GemonitordItem item1 = null, GrafiekWaarde waarde1 = GrafiekWaarde.Vermeldingen,
      GemonitordItem item2 = null, GrafiekWaarde waarde2 = GrafiekWaarde.Vermeldingen,
      GemonitordItem item3 = null, GrafiekWaarde waarde3 = GrafiekWaarde.Vermeldingen,
      GemonitordItem item4 = null, GrafiekWaarde waarde4 = GrafiekWaarde.Vermeldingen,
      GemonitordItem item5 = null, GrafiekWaarde waarde5 = GrafiekWaarde.Vermeldingen)
    {
      GrafiekenManager grafiekenManager = new GrafiekenManager();
      List<GrafiekItem> grafiekItems = new List<GrafiekItem>();
      List<GrafiekWaarde> waarden = new List<GrafiekWaarde>();
      Grafiek grafiek;

      if (item1 != null)
      {
        grafiekItems.Add(new GrafiekItem() { ItemId = item1.GemonitordItemId });
        waarden.Add(waarde1);

        if (item2 != null)
        {
          grafiekItems.Add(new GrafiekItem() { ItemId = item2.GemonitordItemId });
          waarden.Add(waarde2);
        }

        if (item3 != null)
        {
          grafiekItems.Add(new GrafiekItem() { ItemId = item3.GemonitordItemId });
          waarden.Add(waarde3);
        }

        if (item4 != null)
        {
          grafiekItems.Add(new GrafiekItem() { ItemId = item4.GemonitordItemId });
          waarden.Add(waarde4);
        }

        if (item5 != null)
        {
          grafiekItems.Add(new GrafiekItem() { ItemId = item5.GemonitordItemId });
          waarden.Add(waarde5);
        }

        grafiek = new Grafiek()
        {
          GrafiekId = id,
          Titel = titel,
          Periode = periode,
          ToonLegende = toonLegende,
          ToonXAs = toonXAs,
          ToonYAs = toonYAs,
          Type = type,
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
      }
      return RedirectToAction("Index");
    }

    public ActionResult VerwijderGrafiekEnUpdateDashboard(int id)
    {
      GrafiekenManager grafiekenManager = new GrafiekenManager();
      Grafiek grafiek = new Grafiek()
      {
        GrafiekId = id
      };

      grafiekenManager.RemoveGrafiek(grafiek);
      return RedirectToAction("Index");
    }

    public void GetData()
    {
      GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
      DeelplatformenManager deelplatformenManager = new DeelplatformenManager();

      deelplatformenManager.AddDeelplatform(new Deelplatform() { Naam = "Politieke Barometer", AantalDagenHistoriek = 2, LaatsteSynchronisatie = DateTime.Now.AddYears(-100) });
      int id = deelplatformenManager.GetDeelplatformByName("Politieke Barometer").DeelplatformId;
      gemonitordeItemsManager.AddOrganisatie("Open VLD", id, new List<string>() { "Alexander De Croo", "Gwendolyn Rutten", "Maggie De Block" });
      gemonitordeItemsManager.AddOrganisatie("Groen", id, new List<string>() { "Kristof Calvo", "Meyrem Almaci", "Wouter Van Besien" });
      gemonitordeItemsManager.AddOrganisatie("SPA", id, new List<string>() { "Caroline Gennez", "John Crombez", "Bruno Tobback" });
      gemonitordeItemsManager.AddOrganisatie("Vlaams Belang", id, new List<string>() { "Filip Dewinter", "Tom Van Grieken", "Gerolf Annemans" });

      gemonitordeItemsManager.AddThema("Migratie", new List<string>() { "buitenland", "vluchteling", "immigratie", "migratie" }, id);
      gemonitordeItemsManager.AddThema("Fiscaliteit", new List<string>() { "belastingen", "tax", "btw", "sociale zekerheid" }, id);
      gemonitordeItemsManager.AddThema("Milieu", new List<string>() { "kernenergie", "zonnenergie", "steenkool", "luchtvervuiling", "windenergie" }, id);
      TextgainController textgainController = new TextgainController();
    }

  }
}