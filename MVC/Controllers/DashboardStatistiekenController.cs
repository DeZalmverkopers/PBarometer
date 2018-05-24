using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Gemonitordeitems;
using BL;
using Domain.Dashboards;
using Microsoft.AspNet.Identity;
using Domain.Deelplatformen;
using System;

namespace MVC.Controllers
{
  public class DashboardStatistiekenController : Controller
  {

    GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
    List<GemonitordItem> items;
    List<SelectListItem> selects;
    List<GemonitordItem> personenViewbag = new List<GemonitordItem>();
    List<GemonitordItem> organisatiesViewbag = new List<GemonitordItem>();
    List<GemonitordItem> themasViewbag = new List<GemonitordItem>();
    List<Statistiek> statistieken;
    List<GemonitordItem> gemonitordeItems = new List<GemonitordItem>();
    StatistiekenManager statistiekenManager = new StatistiekenManager();
    DashboardsManager dashboardsManager = new DashboardsManager();

    //public Deelplatform HuidigDeelplatform
    //{
    //  get
    //  {
    //    return new DeelplatformenManager().GetDeelplatformByURL(RouteData.Values["deelplatform"].ToString());
    //  }
    //}


    // GET: DashboardStatistieken

    public Deelplatform HuidigDeelplatform
    {
      get
      {
        return new DeelplatformenManager().GetDeelplatformByURL(RouteData.Values["deelplatform"].ToString());
      }
    }
    public Dashboard HuidigDashboard
    {
      get
      {
        return dashboardsManager.GetDashboardVanGebruikerMetGrafieken(User.Identity.GetUserId(), HuidigDeelplatform.DeelplatformId);
      }
    }


    public ActionResult Index()
    {
      //GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();

      if (HuidigDashboard != null)
      {
        var statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId);
        ViewBag.StatistiekenViewbag = statistieken;

      }


      return View();
    }

    public virtual ActionResult LaadGetalKeuze()
    {

      return PartialView("~/Views/Shared/Dashboard/Statistieken/GetalKeuze.cshtml");
    }

    public virtual ActionResult LaadGetalTrendKeuze()
    {

      return PartialView("~/Views/Shared/Dashboard/Statistieken/GetalTrendKeuze.cshtml");
    }

    public virtual ActionResult LaadPopulairsteItemsKeuze()
    {
      return PartialView("~/Views/Shared/Dashboard/Statistieken/PopulairsteItemsKeuze.cshtml");
    }

    public virtual ActionResult LaadStatistiekenNietIngelogd()
    {
      GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();

      var aantalItems = gemonitordeItemsManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList().Count;
      ViewBag.AantalItems = aantalItems;

      if (aantalItems > 0)
      {
        ViewBag.StatistiekenNietIngelogdViewbag = GetStatistiekenTest();

      }

      return PartialView("~/Views/Shared/Dashboard/Statistieken/StatistiekenNietIngelogd.cshtml", ViewBag);

    }


    public ActionResult StatistiekVerwijderen(string id)
    {
      int idInt = Int32.Parse(id);

      StatistiekenManager statistiekenManager = new StatistiekenManager();
      List<Statistiek> statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == idInt)
        {
          statistiekenManager.RemoveStatistiek(statistiek);
        }
      }

      return RedirectToAction("Index");
    }



    public virtual ActionResult LaadKruisingKeuze()
    {
      //items = itemManager.GetPersonen(1).ToList();

      //ViewBag.ItemsViewbag = items;

      var items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.GemonitordeItemsViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Statistieken/GekruistItemKeuze.cshtml", ViewBag);
    }


    public virtual ActionResult LaadGetal(string item)
    {
      //int deelplatformId = HuidigDeelplatform.DeelplatformId;

      //items = itemManager.GetGemonitordeItems(deelplatformId).ToList();

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      GemonitordItem gemonitordItem = new GemonitordItem();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item))
        {
          ViewBag.ItemNaam = element.Naam;
          ViewBag.ItemVermeldingen = element.TotaalAantalVermeldingen;
          gemonitordItem = element;
        }
      }

      Statistiek statistiek = new Statistiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        DashboardId = HuidigDashboard.DashboardId,
        GemonitordItemId = gemonitordItem.GemonitordItemId,
        StatistiekSoort = "getal"

      };


      statistiekenManager.AddStatistiek(statistiek);

      ViewBag.StatistiekId = statistiek.StatistiekId;


      return PartialView("~/Views/Shared/GetalEnOverzicht/Getal.cshtml", ViewBag);
    }


    public virtual ActionResult LaadGetalTrend(string item)
    {
      //int deelplatformId = HuidigDeelplatform.DeelplatformId;

      //items = itemManager.GetGemonitordeItems(deelplatformId).ToList();

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      GemonitordItem gemonitordItem = new GemonitordItem();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item))
        {
          ViewBag.ItemNaam = element.Naam;
          ViewBag.ItemVermeldingen = element.TotaalAantalVermeldingen;

          switch (element.VermeldingenTrend)
          {
            case Trend.DOWN:
              ViewBag.ItemTrend = "Dalend";

              break;
            case Trend.UP:
              ViewBag.ItemTrend = "Stijgend";

              break;
            case Trend.NEUTRAL:
              ViewBag.ItemTrend = "Neutraal";

              break;

          }

          gemonitordItem = element;


        }
      }

      Statistiek statistiek = new Statistiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        DashboardId = HuidigDashboard.DashboardId,
        GemonitordItemId = gemonitordItem.GemonitordItemId,
        StatistiekSoort = "getalTrend"
      };


      statistiekenManager.AddStatistiek(statistiek);

      ViewBag.StatistiekId = statistiek.StatistiekId;


      return PartialView("~/Views/Shared/GetalEnOverzicht/GetalTrend.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop5()
    {

      //int deelplatformId = HuidigDeelplatform.DeelplatformId;

      //items = itemManager.GetGemonitordeItems(deelplatformId).ToList();

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top5 = new List<GemonitordItem>();
      List<string> itemsNamen = new List<string>();
      List<double> itemsWaarden = new List<double>();
      List<string> itemsTrend = new List<string>();

      for (int i = 0; i < 5; i++)
      {
        top5.Add(geordend[i]);

      }

      foreach (var top5Item in top5)
      {
        itemsNamen.Add(top5Item.Naam);
        itemsWaarden.Add(top5Item.TotaalAantalVermeldingen);
        switch (top5Item.VermeldingenTrend)
        {
          case Trend.DOWN:
            itemsTrend.Add("Dalend");
            break;

          case Trend.UP:
            itemsTrend.Add("Stijgend");

            break;

          case Trend.NEUTRAL:
            itemsTrend.Add("Neutraal");

            break;
        }
      }

      ViewBag.ItemsNamen = itemsNamen;
      ViewBag.ItemsWaarden = itemsWaarden;
      ViewBag.ItemsTrend = itemsTrend;

      Statistiek statistiek = new Statistiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        DashboardId = HuidigDashboard.DashboardId,
        StatistiekSoort = "top5"
      };


      statistiekenManager.AddStatistiek(statistiek);

      ViewBag.StatistiekId = statistiek.StatistiekId;


      return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop10()
    {

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top10 = new List<GemonitordItem>();
      List<string> itemsNamen = new List<string>();
      List<double> itemsWaarden = new List<double>();
      List<string> itemsTrend = new List<string>();

      for (int i = 0; i < 10; i++)
      {
        top10.Add(geordend[i]);

      }

      foreach (var top10Item in top10)
      {
        itemsNamen.Add(top10Item.Naam);
        itemsWaarden.Add(top10Item.TotaalAantalVermeldingen);
        switch (top10Item.VermeldingenTrend)
        {
          case Trend.DOWN:
            itemsTrend.Add("Dalend");
            break;

          case Trend.UP:
            itemsTrend.Add("Stijgend");

            break;

          case Trend.NEUTRAL:
            itemsTrend.Add("Neutraal");

            break;
        }
      }

      ViewBag.ItemsNamen = itemsNamen;
      ViewBag.ItemsWaarden = itemsWaarden;
      ViewBag.ItemsTrend = itemsTrend;



      Statistiek statistiek = new Statistiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        DashboardId = HuidigDashboard.DashboardId,
        StatistiekSoort = "top10"
      };


      statistiekenManager.AddStatistiek(statistiek);

      ViewBag.StatistiekId = statistiek.StatistiekId;


      return PartialView("~/Views/Shared/GetalEnOverzicht/Top10.cshtml", ViewBag);
    }

    public virtual ActionResult LaadKruising(string item1, string item2)
    {

      List<GemonitordItem> gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      GemonitordItem gemonitordItem1 = new GemonitordItem();
      GemonitordItem gemonitordItem2 = new GemonitordItem(); ;

      foreach (var gemonitordItem in gemonitordeItems)
      {
        if (gemonitordItem.Naam.Equals(item1))
        {
          gemonitordItem1 = gemonitordItem;
        }
        else if (gemonitordItem.Naam.Equals(item2))
        {
          gemonitordItem2 = gemonitordItem;
        }
      }


      GekruistItem gekruistItem = new GekruistItem()
      {
        Item1 = gemonitordItem1,
        Item2 = gemonitordItem2
      };

      gekruistItem.BerekenEigenschappen();

      var aantal = gekruistItem.TotaalAantalVermeldingen;

      ViewBag.GekruistItemAantal = aantal;
      ViewBag.ItemNaam1 = gemonitordItem1.Naam;
      ViewBag.ItemNaam2 = gemonitordItem2.Naam;


      Statistiek statistiek = new Statistiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        DashboardId = HuidigDashboard.DashboardId,
        GemonitordItemId = gemonitordItem1.GemonitordItemId,
        GemonitordItemId2 = gemonitordItem2.GemonitordItemId,
        StatistiekSoort = "kruising"
      };


      statistiekenManager.AddStatistiek(statistiek);

      ViewBag.StatistiekId = statistiek.StatistiekId;


      return PartialView("~/Views/Shared/GetalEnOverzicht/ItemsKruisen.cshtml", ViewBag);
    }






    public virtual ActionResult LaadStatistiekToevoegen()
    {


      items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.Personen = selects;

      items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.Themas = selects;


      items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.Organisaties = selects;



      return PartialView("~/Views/Shared/Dashboard/Statistieken/StatistiekToevoegen.cshtml", ViewBag);

    }



    //public virtual ActionResult LaadGemonitordeItemsKruising()
    //{
    //  var items = itemManager.GetGemonitordeItems(1).ToList();
    //  selects = new List<SelectListItem>();
    //  foreach (var item in items)
    //  {
    //    selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
    //  }

    //  ViewBag.GemonitordeItemsViewbag = selects;


    //  return PartialView("~/Views/Shared/Dashboard/Dropdown/GemonitordeItemsAantal.cshtml", ViewBag);

    //}



    public virtual ActionResult LaadGetalViaId(int id)
    {
      statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();

      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      //string item = null;
      //int vermeldingen = 0;
      //int statistiekId = 0;

      int gemonitordItemId = 0;

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          //item = statistiek.GemonitordItem.Naam;
          //vermeldingen = statistiek.GemonitordItem.Naam;
          //statistiekId = statistiek.StatistiekId;

          gemonitordItemId = statistiek.GemonitordItemId;

          ViewBag.StatistiekId = statistiek.StatistiekId;
        }
      }

      foreach (var gemonitordItem in gemonitordeItems)
      {
        if (gemonitordItem.GemonitordItemId == gemonitordItemId)
        {
          ViewBag.ItemNaam = gemonitordItem.Naam;
          ViewBag.ItemVermeldingen = gemonitordItem.TotaalAantalVermeldingen;
        }
      }


      return PartialView("~/Views/Shared/GetalEnOverzicht/Getal.cshtml", ViewBag);
    }



    public virtual ActionResult LaadGetalTrendViaId(int id)
    {
      statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      int gemonitordItemId = 0;

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          ViewBag.StatistiekId = statistiek.StatistiekId;
          gemonitordItemId = statistiek.GemonitordItemId;
        }

      }


      foreach (var gemonitordItem in gemonitordeItems)
      {
        if (gemonitordItem.GemonitordItemId == gemonitordItemId)
        {
          ViewBag.Itemnaam = gemonitordItem.Naam;
          ViewBag.ItemVermeldingen = gemonitordItem.TotaalAantalVermeldingen;

          switch (gemonitordItem.VermeldingenTrend)
          {
            case Trend.DOWN:
              ViewBag.ItemTrend = "Dalend";

              break;
            case Trend.UP:
              ViewBag.ItemTrend = "Stijgend";

              break;
            case Trend.NEUTRAL:
              ViewBag.ItemTrend = "Neutraal";

              break;

          }
        }
      }


      return PartialView("~/Views/Shared/GetalEnOverzicht/GetalTrend.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop5ViaId(int id)
    {
      statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top5 = new List<GemonitordItem>();
      List<string> itemsNamen = new List<string>();
      List<double> itemsWaarden = new List<double>();
      List<string> itemsTrend = new List<string>();

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          ViewBag.StatistiekId = statistiek.StatistiekId;
        }

      }


      for (int i = 0; i < 5; i++)
      {
        top5.Add(geordend[i]);

      }

      foreach (var top5Item in top5)
      {
        itemsNamen.Add(top5Item.Naam);
        itemsWaarden.Add(top5Item.TotaalAantalVermeldingen);
        switch (top5Item.VermeldingenTrend)
        {
          case Trend.DOWN:
            itemsTrend.Add("Dalend");
            break;

          case Trend.UP:
            itemsTrend.Add("Stijgend");

            break;

          case Trend.NEUTRAL:
            itemsTrend.Add("Neutraal");

            break;
        }
      }

      ViewBag.ItemsNamen = itemsNamen;
      ViewBag.ItemsWaarden = itemsWaarden;
      ViewBag.ItemsTrend = itemsTrend;




      return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop10ViaId(int id)
    {
      statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          ViewBag.StatistiekId = statistiek.StatistiekId;
        }

      }

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top10 = new List<GemonitordItem>();
      List<string> itemsNamen = new List<string>();
      List<double> itemsWaarden = new List<double>();
      List<string> itemsTrend = new List<string>();

      for (int i = 0; i < 10; i++)
      {
        top10.Add(geordend[i]);

      }

      foreach (var top10Item in top10)
      {
        itemsNamen.Add(top10Item.Naam);
        itemsWaarden.Add(top10Item.TotaalAantalVermeldingen);
        switch (top10Item.VermeldingenTrend)
        {
          case Trend.DOWN:
            itemsTrend.Add("Dalend");
            break;

          case Trend.UP:
            itemsTrend.Add("Stijgend");

            break;

          case Trend.NEUTRAL:
            itemsTrend.Add("Neutraal");

            break;
        }
      }

      ViewBag.ItemsNamen = itemsNamen;
      ViewBag.ItemsWaarden = itemsWaarden;
      ViewBag.ItemsTrend = itemsTrend;


      return PartialView("~/Views/Shared/GetalEnOverzicht/Top10.cshtml", ViewBag);
    }

    public virtual ActionResult LaadKruisingViaId(int id)
    {
      statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      int gemonitordItemId = 0;
      int gemonitordItemId2 = 0;
      GemonitordItem gemonitordItem1 = new GemonitordItem();
      GemonitordItem gemonitordItem2 = new GemonitordItem();

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          ViewBag.StatistiekId = statistiek.StatistiekId;
          gemonitordItemId = statistiek.GemonitordItemId;
          gemonitordItemId2 = statistiek.GemonitordItemId2;
        }

      }

      foreach (var item in gemonitordeItems)
      {
        if (item.GemonitordItemId == gemonitordItemId)
        {
          gemonitordItem1 = item;
        }
        if (item.GemonitordItemId == gemonitordItemId2)
        {
          gemonitordItem2 = item;

        }
      }

      GekruistItem gekruistItem = new GekruistItem()
      {
        Item1 = gemonitordItem1,
        Item2 = gemonitordItem2
      };

      gekruistItem.BerekenEigenschappen();

      var aantal = gekruistItem.TotaalAantalVermeldingen;

      ViewBag.GekruistItemAantal = aantal;
      ViewBag.ItemNaam1 = gemonitordItem1.Naam;
      ViewBag.ItemNaam2 = gemonitordItem2.Naam;



      return PartialView("~/Views/Shared/GetalEnOverzicht/ItemsKruisen.cshtml", ViewBag);
    }






    public virtual ActionResult LaadGetalViaIdNietOpslaan(int id)
    {
      statistieken = GetStatistiekenTest();

      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      //string item = null;
      //int vermeldingen = 0;
      //int statistiekId = 0;

      int gemonitordItemId = 0;

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekIdNietOpslaan == id)
        {
          //item = statistiek.GemonitordItem.Naam;
          //vermeldingen = statistiek.GemonitordItem.Naam;
          //statistiekId = statistiek.StatistiekId;

          gemonitordItemId = statistiek.GemonitordItemId;

          ViewBag.StatistiekId = statistiek.StatistiekIdNietOpslaan;
        }
      }

      foreach (var gemonitordItem in gemonitordeItems)
      {
        if (gemonitordItem.GemonitordItemId == gemonitordItemId)
        {
          ViewBag.ItemNaam = gemonitordItem.Naam;
          ViewBag.ItemVermeldingen = gemonitordItem.TotaalAantalVermeldingen;
        }
      }


      return PartialView("~/Views/Shared/GetalEnOverzicht/Getal.cshtml", ViewBag);
    }



    public virtual ActionResult LaadGetalTrendViaIdNietOpslaan(int id)
    {
      statistieken = GetStatistiekenTest();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      int gemonitordItemId = 0;

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekIdNietOpslaan == id)
        {
          ViewBag.StatistiekId = statistiek.StatistiekIdNietOpslaan;
          gemonitordItemId = statistiek.GemonitordItemId;
        }

      }


      foreach (var gemonitordItem in gemonitordeItems)
      {
        if (gemonitordItem.GemonitordItemId == gemonitordItemId)
        {
          ViewBag.Itemnaam = gemonitordItem.Naam;
          ViewBag.ItemVermeldingen = gemonitordItem.TotaalAantalVermeldingen;


          ViewBag.ItemTrend = gemonitordItem.VermeldingenTrend;

          switch (gemonitordItem.VermeldingenTrend)
          {
            case Trend.DOWN:
              ViewBag.ItemTrend = "Dalend";

              break;
            case Trend.UP:
              ViewBag.ItemTrend = "Stijgend";

              break;
            case Trend.NEUTRAL:
              ViewBag.ItemTrend = "Neutraal";

              break;

          }
        }
      }


      return PartialView("~/Views/Shared/GetalEnOverzicht/GetalTrend.cshtml", ViewBag);
    }





    public virtual ActionResult LaadKruisingViaIdNietOpslaan(int id)
    {
      statistieken = GetStatistiekenTest();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      int gemonitordItemId = 0;
      int gemonitordItemId2 = 0;
      GemonitordItem gemonitordItem1 = new GemonitordItem();
      GemonitordItem gemonitordItem2 = new GemonitordItem();

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekIdNietOpslaan == id)
        {
          ViewBag.StatistiekId = statistiek.StatistiekIdNietOpslaan;
          gemonitordItemId = statistiek.GemonitordItemId;
          gemonitordItemId2 = statistiek.GemonitordItemId2;
        }

      }

      foreach (var item in gemonitordeItems)
      {
        if (item.GemonitordItemId == gemonitordItemId)
        {
          gemonitordItem1 = item;
        }
        if (item.GemonitordItemId == gemonitordItemId2)
        {
          gemonitordItem2 = item;

        }
      }

      GekruistItem gekruistItem = new GekruistItem()
      {
        Item1 = gemonitordItem1,
        Item2 = gemonitordItem2
      };

      gekruistItem.BerekenEigenschappen();

      var aantal = gekruistItem.TotaalAantalVermeldingen;

      ViewBag.GekruistItemAantal = aantal;
      ViewBag.ItemNaam1 = gemonitordItem1.Naam;
      ViewBag.ItemNaam2 = gemonitordItem2.Naam;



      return PartialView("~/Views/Shared/GetalEnOverzicht/ItemsKruisen.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop5NietOpslaan()
    {
      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top5 = new List<GemonitordItem>();
      List<string> itemsNamen = new List<string>();
      List<double> itemsWaarden = new List<double>();
      List<string> itemsTrend = new List<string>();


      for (int i = 0; i < 5; i++)
      {
        top5.Add(geordend[i]);

      }

      foreach (var top5Item in top5)
      {
        itemsNamen.Add(top5Item.Naam);
        itemsWaarden.Add(top5Item.TotaalAantalVermeldingen);
        switch (top5Item.VermeldingenTrend)
        {
          case Trend.DOWN:
            itemsTrend.Add("Dalend");
            break;

          case Trend.UP:
            itemsTrend.Add("Stijgend");

            break;

          case Trend.NEUTRAL:
            itemsTrend.Add("Neutraal");

            break;
        }
      }

      ViewBag.ItemsNamen = itemsNamen;
      ViewBag.ItemsWaarden = itemsWaarden;
      ViewBag.ItemsTrend = itemsTrend;




      return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop10NietOpslaan()
    {

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top10 = new List<GemonitordItem>();
      List<string> itemsNamen = new List<string>();
      List<double> itemsWaarden = new List<double>();
      List<string> itemsTrend = new List<string>();

      for (int i = 0; i < 10; i++)
      {
        top10.Add(geordend[i]);

      }

      foreach (var top10Item in top10)
      {
        itemsNamen.Add(top10Item.Naam);
        itemsWaarden.Add(top10Item.TotaalAantalVermeldingen);
        switch (top10Item.VermeldingenTrend)
        {
          case Trend.DOWN:
            itemsTrend.Add("Dalend");
            break;

          case Trend.UP:
            itemsTrend.Add("Stijgend");

            break;

          case Trend.NEUTRAL:
            itemsTrend.Add("Neutraal");

            break;
        }
      }

      ViewBag.ItemsNamen = itemsNamen;
      ViewBag.ItemsWaarden = itemsWaarden;
      ViewBag.ItemsTrend = itemsTrend;


      return PartialView("~/Views/Shared/GetalEnOverzicht/Top10.cshtml", ViewBag);
    }

    public List<Statistiek> GetStatistiekenTest()
    {
      GemonitordeItemsManager itemsManager = new GemonitordeItemsManager();
      List<GemonitordItem> personen = itemsManager.GetPersonen(HuidigDeelplatform.DeelplatformId).OrderByDescending(p => p.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> organisaties = itemsManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).OrderByDescending(p => p.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> themas = itemsManager.GetThemas(HuidigDeelplatform.DeelplatformId).OrderByDescending(p => p.TotaalAantalVermeldingen).ToList();

      List<Statistiek> statistieken = new List<Statistiek>()
      {
        new Statistiek()
        {
          //StatistiekId    = 1,

          StatistiekIdNietOpslaan = 1,
          GemonitordItemId  = personen[0].GemonitordItemId,
          //DashboardId = 1,
          //DeelplatformId = 1,
          StatistiekSoort = "getal"
        },
        new Statistiek()
        {
          StatistiekIdNietOpslaan = 2,
          //StatistiekId   = 2,
          GemonitordItemId = organisaties[0].GemonitordItemId,
          //DashboardId = 1,
          //DeelplatformId = 1,
          StatistiekSoort = "getal"
        },
        new Statistiek()
        {
           StatistiekIdNietOpslaan = 3,
          //StatistiekId   = 3,
          GemonitordItemId = personen[1].GemonitordItemId,
          //DashboardId = 1,
          //DeelplatformId = 1,
          StatistiekSoort = "getalTrend"
        },
        new Statistiek()
        {
           StatistiekIdNietOpslaan = 4,
          //StatistiekId   = 4,
          GemonitordItemId = organisaties[1].GemonitordItemId,
          //DashboardId = 1,
          //DeelplatformId = 1,
          StatistiekSoort = "getalTrend"

        },

        new Statistiek()
        {
          StatistiekIdNietOpslaan = 5,
          //StatistiekId   = 5,
          GemonitordItemId = personen[0].GemonitordItemId,
          GemonitordItemId2 = organisaties[0].GemonitordItemId,
          StatistiekSoort = "kruising"

        }

      };

      return statistieken;
    }

  }
}