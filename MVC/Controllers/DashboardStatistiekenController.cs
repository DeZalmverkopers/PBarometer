using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Gemonitordeitems;
using BL;
using Domain.Deelplatformen;
using Domain.Dashboards;

namespace MVC.Controllers
{
  public class DashboardStatistiekenController : Controller
  {

    GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
    List<GemonitordItem> items;
    List<SelectListItem> selects;
    List<Thema> themas;
    List<GemonitordItem> personenViewbag = new List<GemonitordItem>();
    List<GemonitordItem> organisatiesViewbag = new List<GemonitordItem>();
    List<GemonitordItem> themasViewbag = new List<GemonitordItem>();
    List<Statistiek> statistieken;
    List<GemonitordItem> gemonitordeItems = new List<GemonitordItem>();
    StatistiekenManager statistiekenManager = new StatistiekenManager();

    //public Deelplatform HuidigDeelplatform
    //{
    //  get
    //  {
    //    return new DeelplatformenManager().GetDeelplatformByURL(RouteData.Values["deelplatform"].ToString());
    //  }
    //}


    // GET: DashboardStatistieken
    public ActionResult Index()
    {
      GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
      statistieken = statistiekenManager.GetStatistieken();

      //List<Statistiek> statistieken = new List<Statistiek>()
      //{
      //  new Statistiek()
      //  {
      //    StatistiekId   = 1,
      //    GemonitordItem = new GemonitordItem()
      //    {
      //      Naam = "gi1"
      //    },
      //    SoortStatistiek = "getal"
      //  },
      //  new Statistiek()
      //  {
      //    StatistiekId = 2,
      //    GemonitordItem = new GemonitordItem()
      //    {
      //      Naam = "gi1"
      //    },
      //    SoortStatistiek = "getalTrend"
      //  }
      //};

      ViewBag.StatistiekenViewbag = statistieken;

      return View();
    }

    public virtual ActionResult LaadGetalKeuze()
    {

      return PartialView("~/Views/Shared/Dashboard/Statistieken/GetalKeuze.cshtml", ViewBag);
    }

    public virtual ActionResult LaadGetalTrendKeuze()
    {

      return PartialView("~/Views/Shared/Dashboard/Statistieken/GetalTrendKeuze.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop5Keuze()
    {

      return PartialView("~/Views/Shared/Dashboard/Statistieken/Top5Keuze.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop10Keuze()
    {


      return PartialView("~/Views/Shared/Dashboard/Statistieken/Top10Keuze.cshtml", ViewBag);
    }




    public virtual ActionResult LaadGetal(string item)
    {
      //int deelplatformId = HuidigDeelplatform.DeelplatformId;

      //items = itemManager.GetGemonitordeItems(deelplatformId).ToList();

      items = itemManager.GetGemonitordeItems(1).ToList();

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
        StatistiekId = 9,
        GemonitordItem = gemonitordItem,
        SoortStatistiek = "getal"
      };

      ViewBag.StatistiekId = statistiek.StatistiekId;

      return PartialView("~/Views/Shared/GetalEnOverzicht/Getal.cshtml", ViewBag);
    }


    public virtual ActionResult LaadGetalTrend(string item)
    {
      //int deelplatformId = HuidigDeelplatform.DeelplatformId;

      //items = itemManager.GetGemonitordeItems(deelplatformId).ToList();

      items = itemManager.GetGemonitordeItems(1).ToList();


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
        StatistiekId = 10,
        GemonitordItem = gemonitordItem,
        SoortStatistiek = "getalTrend"
      };

      ViewBag.StatistiekId = statistiek.StatistiekId;

      return PartialView("~/Views/Shared/GetalEnOverzicht/GetalTrend.cshtml", ViewBag);
    }


    public virtual ActionResult LaadTop5(string item)
    {

      //int deelplatformId = HuidigDeelplatform.DeelplatformId;

      //items = itemManager.GetGemonitordeItems(deelplatformId).ToList();

      items = itemManager.GetGemonitordeItems(1).ToList();


      GemonitordItem gemonitordItem = new GemonitordItem();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item))
        {
          ViewBag.ItemNaam = element.Naam;
          
          gemonitordItem = element;

        }
      }

      Statistiek statistiek = new Statistiek()
      {
        StatistiekId = 11,
        GemonitordItem = gemonitordItem,
        SoortStatistiek = "top5"
      };

      ViewBag.StatistiekId = statistiek.StatistiekId;

      
      return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop10(string item)
    {
      //int deelplatformId = HuidigDeelplatform.DeelplatformId;

      //items = itemManager.GetGemonitordeItems(deelplatformId).ToList();

      items = itemManager.GetGemonitordeItems(1).ToList();


      GemonitordItem gemonitordItem = new GemonitordItem();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item))
        {
          ViewBag.ItemNaam = element.Naam;

          gemonitordItem = element;

        }
      }

      Statistiek statistiek = new Statistiek()
      {
        StatistiekId = 12,
        GemonitordItem = gemonitordItem,
        SoortStatistiek = "top10"
      };

      ViewBag.StatistiekId = statistiek.StatistiekId;


      return PartialView("~/Views/Shared/GetalEnOverzicht/Top10.cshtml", ViewBag);
    }



    public virtual ActionResult LaadStatistiekToevoegen()
    {


      items = itemManager.GetPersonen(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.Personen = selects;

      themas = itemManager.GetThemas(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in themas)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.Themas = selects;


      items = itemManager.GetOrganisaties(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.Organisaties = selects;

      return PartialView("~/Views/Shared/Dashboard/Statistieken/StatistiekToevoegen.cshtml", ViewBag);

    }



    public virtual ActionResult LaadGetalViaId(int id)
    {
      statistieken = statistiekenManager.GetStatistieken();

      //string item = null;
      //int vermeldingen = 0;
      //int statistiekId = 0;

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          //item = statistiek.GemonitordItem.Naam;
          //vermeldingen = statistiek.GemonitordItem.Naam;
          //statistiekId = statistiek.StatistiekId;

          ViewBag.Itemnaam = statistiek.GemonitordItem.Naam;
          ViewBag.ItemVermeldingen = statistiek.GemonitordItem.TotaalAantalVermeldingen;

          ViewBag.StatistiekId = statistiek.StatistiekId;

        }
      }



      return PartialView("~/Views/Shared/GetalEnOverzicht/Getal.cshtml", ViewBag);
    }



    public virtual ActionResult LaadGetalTrendViaId(int id)
    {
      statistieken = statistiekenManager.GetStatistieken();

      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          ViewBag.StatistiekId = statistiek.StatistiekId;

          ViewBag.Itemnaam = statistiek.GemonitordItem.Naam;
          ViewBag.ItemVermeldingen = statistiek.GemonitordItem.TotaalAantalVermeldingen;

          switch (statistiek.GemonitordItem.VermeldingenTrend)
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
      statistieken = statistiekenManager.GetStatistieken();


      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          ViewBag.Itemnaam = statistiek.GemonitordItem.Naam;
          ViewBag.StatistiekId = statistiek.StatistiekId;

        }
      }

      return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop10ViaId(int id)
    {
      statistieken = statistiekenManager.GetStatistieken();


      foreach (var statistiek in statistieken)
      {
        if (statistiek.StatistiekId == id)
        {
          ViewBag.Itemnaam = statistiek.GemonitordItem.Naam;
          ViewBag.StatistiekId = statistiek.StatistiekId;

        }
      }

      return PartialView("~/Views/Shared/GetalEnOverzicht/Top10.cshtml", ViewBag);
    }


    //public virtual ActionResult LaadGetalViaId(int id)
    //{

    //  string item = null;
    //  int vermeldingen = 0;
    //  int statistiekId = 0;

    //  foreach (var statistiek in statistieken)
    //  {
    //    if (statistiek.StatistiekId == 1)
    //    {
    //      item = statistiek.GemonitordItem.Naam;
    //      vermeldingen = statistiek.GemonitordItem.TotaalAantalVermeldingen;
    //      statistiekId = statistiek.StatistiekId;
    //    }
    //  }

    //  ViewBag.Itemnaam = item;
    //  ViewBag.ItemVermeldingen = vermeldingen;

    //  ViewBag.StatisitekId = statistiekId;


    //  return PartialView("~/Views/Shared/GetalEnOverzicht/Getal.cshtml", ViewBag);
    //}



    //public virtual ActionResult LaadGetalTrendViaId(int id)
    //{


    //  foreach (var statistiek in statistieken)
    //  {
    //    if (statistiek.StatistiekId == id)
    //    {
    //      ViewBag.Itemnaam = statistiek.GemonitordItem.Naam;
    //      ViewBag.ItemVermeldingen = statistiek.GemonitordItem.TotaalAantalVermeldingen;

    //      switch (statistiek.GemonitordItem.VermeldingenTrend)
    //      {
    //        case Trend.DOWN:
    //          ViewBag.ItemTrend = "Dalend";

    //          break;
    //        case Trend.UP:
    //          ViewBag.ItemTrend = "Stijgend";

    //          break;
    //        case Trend.NEUTRAL:
    //          ViewBag.ItemTrend = "Neutraal";

    //          break;

    //      }

    //      ViewBag.StatisitekId = statistiek.StatistiekId;

    //    }
    //  }

    //  return PartialView("~/Views/Shared/GetalEnOverzicht/GetalTrend.cshtml", ViewBag);
    //}



    //public virtual ActionResult LaadTop5ViaId(int id)
    //{
    //  foreach (var statistiek in statistieken)
    //  {
    //    if (statistiek.StatistiekId == id)
    //    {
    //      ViewBag.Itemnaam = statistiek.GemonitordItem.Naam;
    //      ViewBag.StatisitekId = statistiek.StatistiekId;

    //    }
    //  }

    //  return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml", ViewBag);
    //}

    //public virtual ActionResult LaadTop10ViaId(int id)
    //{
    //  foreach (var statistiek in statistieken)
    //  {
    //    if (statistiek.StatistiekId == id)
    //    {
    //      ViewBag.Itemnaam = statistiek.GemonitordItem.Naam;
    //      ViewBag.StatisitekId = statistiek.StatistiekId;

    //    }
    //  }

    //  return PartialView("~/Views/Shared/GetalEnOverzicht/Top10.cshtml", ViewBag);
    //}

  }
}