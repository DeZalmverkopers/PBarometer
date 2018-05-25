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
    List<GemonitordItem> items = new List<GemonitordItem>();
    List<SelectListItem> selects = new List<SelectListItem>();
    List<GemonitordItem> personenViewbag = new List<GemonitordItem>();
    List<GemonitordItem> organisatiesViewbag = new List<GemonitordItem>();
    List<GemonitordItem> themasViewbag = new List<GemonitordItem>();
    List<Statistiek> statistieken = new List<Statistiek>();
    List<GemonitordItem> gemonitordeItems = new List<GemonitordItem>();
    StatistiekenManager statistiekenManager = new StatistiekenManager();
    DashboardsManager dashboardsManager = new DashboardsManager();



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
      ViewBag.DeelplatformNaam = HuidigDeelplatform.Naam;

      if (HuidigDashboard != null)
      {
        var statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId);
        ViewBag.StatistiekenViewbag = statistieken;

      }

      ViewBag.AantalThemas = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).Count();


      return View();
    }



    #region Statistiek: toevoegen & verwijderen
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
    #endregion



    #region keuzeStatistiek
    public virtual ActionResult LaadGetalKeuze()
    {
      ViewBag.AantalThemas = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).Count();

      return PartialView("~/Views/Shared/Dashboard/Statistieken/GetalKeuze.cshtml", ViewBag);
    }

    public virtual ActionResult LaadGetalTrendKeuze()
    {
      ViewBag.AantalThemas = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).Count();

      return PartialView("~/Views/Shared/Dashboard/Statistieken/GetalTrendKeuze.cshtml", ViewBag);
    }

    public virtual ActionResult LaadPopulairsteItemsKeuze()
    {
      return PartialView("~/Views/Shared/Dashboard/Statistieken/PopulairsteItemsKeuze.cshtml");
    }

    public virtual ActionResult LaadKruisingKeuze()
    {
      return PartialView("~/Views/Shared/Dashboard/Statistieken/GekruistItemKeuze.cshtml");
    }
    #endregion



    #region Statistieken: weergave & persistentie

    public virtual ActionResult LaadPersisteerGetal(string id1)
    {
      int id1Int = Int32.Parse(id1);

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      GemonitordItem gemonitordItem = new GemonitordItem();

      foreach (var element in items)
      {
        if (element.GemonitordItemId == id1Int)
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


    public virtual ActionResult LaadPersisteerGetalTrend(string id1)
    {
      int id1Int = Int32.Parse(id1);

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      GemonitordItem gemonitordItem = new GemonitordItem();

      foreach (var element in items)
      {
        if (element.GemonitordItemId == id1Int)
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



    public virtual ActionResult LaadPersisteerTop5()
    {

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      List<GemonitordItem> itemsZonderKruisingen = new List<GemonitordItem>();

      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top5 = new List<GemonitordItem>();
      List<string> itemsNamen = new List<string>();
      List<double> itemsWaarden = new List<double>();
      List<string> itemsTrend = new List<string>();

      foreach (var item in geordend)
      {
        if (!(item is GekruistItem))
        {
          itemsZonderKruisingen.Add(item);
        }
      }

      for (int i = 0; i < 5; i++)
      {

        top5.Add(itemsZonderKruisingen[i]);

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



    public virtual ActionResult LaadPersisteerTop10()
    {

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<GemonitordItem> itemsZonderKruisingen = new List<GemonitordItem>();
      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top10 = new List<GemonitordItem>();
      List<string> itemsNamen = new List<string>();
      List<double> itemsWaarden = new List<double>();
      List<string> itemsTrend = new List<string>();

      foreach (var item in geordend)
      {
        if (!(item is GekruistItem))
        {
          itemsZonderKruisingen.Add(item);
        }
      }

      for (int i = 0; i < 10; i++)
      {

        top10.Add(itemsZonderKruisingen[i]);

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

    public virtual ActionResult LaadPersisteerKruising(string id1, string id2)
    {
      int id1Int = Int32.Parse(id1);
      int id2Int = Int32.Parse(id2);

      List<GemonitordItem> gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      GemonitordItem gemonitordItem1 = new GemonitordItem();
      GemonitordItem gemonitordItem2 = new GemonitordItem(); 

      GemonitordItem gekruistItem = new GekruistItem();

      foreach (var gemonitordItem in gemonitordeItems)
      {
        if (gemonitordItem.GemonitordItemId == id1Int)
        {
          gemonitordItem1 = gemonitordItem;
        }
        else if (gemonitordItem.GemonitordItemId == id2Int)
        {
          gemonitordItem2 = gemonitordItem;
        }
      }


      itemManager.AddGekruistItem(gemonitordItem1, gemonitordItem2, gemonitordItem1.Naam + " & " + gemonitordItem2.Naam, HuidigDeelplatform.DeelplatformId);


      int index = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList().Count - 1;

      gekruistItem = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList()[index];


      itemManager.BepaalDetailItemsVoorGekruistItem(gekruistItem as GekruistItem);
      //itemManager.MaakHistorieken(gekruistItem, HuidigDeelplatform.AantalDagenHistoriek, HuidigDeelplatform.LaatsteSynchronisatie);
      itemManager.BerekenEigenschappen(gekruistItem);
      itemManager.ChangeGemonitordItem(gekruistItem);

      var aantal = gekruistItem.TotaalAantalVermeldingen;

      Statistiek statistiek = new Statistiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        DashboardId = HuidigDashboard.DashboardId,
        GemonitordItemId = gekruistItem.GemonitordItemId,
        StatistiekSoort = "kruising"
      };


      statistiekenManager.AddStatistiek(statistiek);

      ViewBag.StatistiekId = statistiek.StatistiekId;
      ViewBag.GekruistItemAantal = aantal;
      ViewBag.ItemNaam1 = gemonitordItem1.Naam;
      ViewBag.ItemNaam2 = gemonitordItem2.Naam;

      return PartialView("~/Views/Shared/GetalEnOverzicht/ItemsKruisen.cshtml", ViewBag);
    }
    #endregion



    #region Statistieken: inladen uit databank

    public virtual ActionResult LaadGetalUitDatabank(int id)
    {
      statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();

      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();


      int gemonitordItemId = 0;

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {


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


    public virtual ActionResult LaadGetalTrendUitDatabank(int id)
    {
      statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      int gemonitordItemId = 0;

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          gemonitordItemId = statistiek.GemonitordItemId;
          ViewBag.StatistiekId = statistiek.StatistiekId;
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

    public virtual ActionResult LaadTop5UitDatabank(int id)
    {
      statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<GemonitordItem> itemsZonderKruisingen = new List<GemonitordItem>();
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


      foreach (var item in geordend)
      {
        if (!(item is GekruistItem))
        {
          itemsZonderKruisingen.Add(item);
        }
      }

      for (int i = 0; i < 5; i++)
      {

        top5.Add(itemsZonderKruisingen[i]);

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

    public virtual ActionResult LaadTop10UitDatabank(int id)
    {
      statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<GemonitordItem> itemsZonderKruisingen = new List<GemonitordItem>();
      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top10 = new List<GemonitordItem>();
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



      foreach (var item in geordend)
      {
        if (!(item is GekruistItem))
        {
          itemsZonderKruisingen.Add(item);
        }
      }

      for (int i = 0; i < 10; i++)
      {

        top10.Add(itemsZonderKruisingen[i]);

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

    public virtual ActionResult LaadKruisingUitDatabank(int id)
    {
      statistieken = statistiekenManager.GetStatistieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId).ToList();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      List<GekruistItem> gekruisteItems = new List<GekruistItem>();
      GemonitordItem gekruistItem = new GekruistItem();

      string naam1 = "";
      string naam2 = "";
      int gekruistItemId = 0;

      foreach (var item in gemonitordeItems)
      {
        if (item is GekruistItem)
        {
          gekruisteItems.Add(item as GekruistItem);
        }
      }

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          ViewBag.StatistiekId = statistiek.StatistiekId;
          gekruistItemId = statistiek.GemonitordItemId;
        }

      }

      foreach (var item in gekruisteItems)
      {
        if (item.GemonitordItemId == gekruistItemId)
        {
          gekruistItem = item as GekruistItem;
          naam1 = item.Item1.Naam;
          naam2 = item.Item2.Naam;
        }
       
      }
      

      var aantal = gekruistItem.TotaalAantalVermeldingen;

      ViewBag.GekruistItemAantal = aantal;
      ViewBag.ItemNaam1 = naam1;
      ViewBag.ItemNaam2 = naam2;


      return PartialView("~/Views/Shared/GetalEnOverzicht/ItemsKruisen.cshtml", ViewBag);
    }


    #endregion



    #region Statistieken die niet worden opgeslagen
    public virtual ActionResult LaadGetalNietOpslaan(int id)
    {
      statistieken = GetStatistiekenNietIngelogd();

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



    public virtual ActionResult LaadGetalTrendNietOpslaan(int id)
    {
      statistieken = GetStatistiekenNietIngelogd();
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





    public virtual ActionResult LaadKruisingNietOpslaan(int id)
    {
      statistieken = GetStatistiekenNietIngelogd();
      gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      int gemonitordItemId = 0;
      int gemonitordItemId2 = 0;
      GemonitordItem gemonitordItem1 = new GemonitordItem();
      GemonitordItem gemonitordItem2 = new GemonitordItem();
      GemonitordItem gekruistItem = new GekruistItem();

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

      itemManager.AddGekruistItem(gemonitordItem1, gemonitordItem2, gemonitordItem1.Naam + " & " + gemonitordItem2.Naam, HuidigDeelplatform.DeelplatformId);


      int index = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList().Count - 1;

      gekruistItem = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList()[index];


      itemManager.BepaalDetailItemsVoorGekruistItem(gekruistItem as GekruistItem);
      //itemManager.MaakHistorieken(gekruistItem, HuidigDeelplatform.AantalDagenHistoriek, HuidigDeelplatform.LaatsteSynchronisatie);
      itemManager.BerekenEigenschappen(gekruistItem);
      itemManager.ChangeGemonitordItem(gekruistItem);

      var aantal = gekruistItem.TotaalAantalVermeldingen;

      ViewBag.GekruistItemAantal = aantal;
      ViewBag.ItemNaam1 = gemonitordItem1.Naam;
      ViewBag.ItemNaam2 = gemonitordItem2.Naam;

      return PartialView("~/Views/Shared/GetalEnOverzicht/ItemsKruisen.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop5NietOpslaan()
    {
      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<GemonitordItem> itemsZonderKruisingen = new List<GemonitordItem>();
      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top5 = new List<GemonitordItem>();
      List<string> itemsNamen = new List<string>();
      List<double> itemsWaarden = new List<double>();
      List<string> itemsTrend = new List<string>();


      foreach (var item in geordend)
      {
        if (!(item is GekruistItem))
        {
          itemsZonderKruisingen.Add(item);
        }
      }

      for (int i = 0; i < 5; i++)
      {

        top5.Add(itemsZonderKruisingen[i]);

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

      List<GemonitordItem> itemsZonderKruisingen = new List<GemonitordItem>();
      List<GemonitordItem> geordend = items.OrderByDescending(i => i.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> top10 = new List<GemonitordItem>();
      List<string> itemsNamen = new List<string>();
      List<double> itemsWaarden = new List<double>();
      List<string> itemsTrend = new List<string>();

      foreach (var item in geordend)
      {
        if (!(item is GekruistItem))
        {
          itemsZonderKruisingen.Add(item);
        }
      }

      for (int i = 0; i < 10; i++)
      {

        top10.Add(itemsZonderKruisingen[i]);

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
    #endregion



    #region statistieken voor niet-ingelogde gebruikers

    //Voorbeeldgrafieken voor een niet-ingelogde gebruiker
    public virtual ActionResult LaadStatistiekenNietIngelogd()
    {
      GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();

      var aantalItems = gemonitordeItemsManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList().Count;
      ViewBag.AantalItems = aantalItems;

      if (aantalItems > 0)
      {
        ViewBag.StatistiekenNietIngelogdViewbag = GetStatistiekenNietIngelogd();

      }

      return PartialView("~/Views/Shared/Dashboard/Statistieken/StatistiekenNietIngelogd.cshtml", ViewBag);

    }


    public List<Statistiek> GetStatistiekenNietIngelogd()
    {
      GemonitordeItemsManager itemsManager = new GemonitordeItemsManager();
      List<GemonitordItem> personen = itemsManager.GetPersonen(HuidigDeelplatform.DeelplatformId).OrderByDescending(p => p.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> organisaties = itemsManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).OrderByDescending(p => p.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> themas = itemsManager.GetThemas(HuidigDeelplatform.DeelplatformId).OrderByDescending(p => p.TotaalAantalVermeldingen).ToList();

      List<Statistiek> statistieken = new List<Statistiek>()
      {
        new Statistiek()
        {

          StatistiekIdNietOpslaan = 1,
          GemonitordItemId  = personen[0].GemonitordItemId,
          StatistiekSoort = "getal"
        },
        new Statistiek()
        {
          StatistiekIdNietOpslaan = 2,
          GemonitordItemId = organisaties[0].GemonitordItemId,
          StatistiekSoort = "getal"
        },
        new Statistiek()
        {
           StatistiekIdNietOpslaan = 3,
          GemonitordItemId = personen[1].GemonitordItemId,
          StatistiekSoort = "getalTrend"
        },
        new Statistiek()
        {
           StatistiekIdNietOpslaan = 4,
          GemonitordItemId = organisaties[1].GemonitordItemId,
          StatistiekSoort = "getalTrend"

        },

        new Statistiek()
        {
          StatistiekIdNietOpslaan = 5,
          GemonitordItemId = personen[0].GemonitordItemId,
          GemonitordItemId2 = organisaties[0].GemonitordItemId,

          StatistiekSoort = "kruising"

        }

      };

      return statistieken;
    }
    #endregion



  }
}