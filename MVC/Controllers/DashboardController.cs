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
    List<GemonitordItem> items;
    //List<Thema> themas;
    HomeController homeController = new HomeController();
    public Deelplatform HuidigDeelplatform
    {
      get
      {
        return new DeelplatformenManager().GetDeelplatformByURL(RouteData.Values["deelplatform"].ToString());
      }
    }
    private int deelplatformId;
    // GET: Dashboard



    public virtual ActionResult Index()
    {
      


      if (HuidigDeelplatform == null)
      {
        return RedirectToAction("Index", "Home");
      }
      //homeController.GetData();
      //GetData();
      deelplatformId = HuidigDeelplatform.DeelplatformId;
      GrafiekenManager grafiekenManager = new GrafiekenManager();
      //List<Grafiek> grafieken = grafiekenManager.GetGrafieken(deelplatformId, 1, true, true);


      ViewBag.DeelplatformNaam = HuidigDeelplatform.Naam;
      ViewBag.Afbeelding = HuidigDeelplatform.AfbeeldingPad ?? "default.png";






      List<GemonitordItem> personen = itemManager.GetPersonen(1).ToList();
      //items = itemManager.GetGemonitordeItems(1).ToList();

   



      List<dynamic> grafiek1XLabels = new List<dynamic>();
      List<double> grafiek1Datawaarden = new List<double>();
      //for (int i = 0; i < 5; i++)
      //{
      //  grafiek1XLabels.Add(personen[i].Naam);
      //  grafiek1Datawaarden.Add(personen[i].GemPolariteit);
      //}
      for (int i = 0; i < personen.Count; i++)
      {
        grafiek1XLabels.Add(personen[i].Naam);
        grafiek1Datawaarden.Add(personen[i].GemPolariteit);
      }




      List<ItemHistoriek> grafiek2Itemhistorieken = new List<ItemHistoriek>();
      List<dynamic> grafiek2XLabels = new List<dynamic>();
      List<double> grafiek2Waarden = new List<double>();

      List<GemonitordItem> personen2 = itemManager.GetPersonen(1).ToList();

      for (int i = 0; i < personen2.Count; i++)
      {
        if (personen2[0].GemonitordItemId == 2)
        {
          grafiek2Itemhistorieken = personen2[i].ItemHistorieken;

        }
      }



      //for (int i = grafiek2Itemhistorieken.Count - 13; i < grafiek2Itemhistorieken.Count; i++)
      //{
      //  grafiek2XLabels.Add(grafiek2Itemhistorieken[i].HistoriekDatum.ToShortDateString());
      //  grafiek2Waarden.Add(grafiek2Itemhistorieken[i].AantalVermeldingen);
      //}
      for (int i = 0; i < grafiek2Itemhistorieken.Count; i++)
      {
        grafiek2XLabels.Add(grafiek2Itemhistorieken[i].HistoriekDatum.ToShortDateString());
        grafiek2Waarden.Add(grafiek2Itemhistorieken[i].AantalVermeldingen);
      }



      List<GemonitordItem> organisaties = itemManager.GetOrganisaties(1).ToList();
      List<dynamic> grafiek3XLabels = new List<dynamic>();
      List<ItemHistoriek> grafiek3Itemhistorieken = new List<ItemHistoriek>();
      List<double> grafiek3Waarden = new List<double>();

      //items = itemManager.GetGemonitordeItems(deelplatformId).ToList();
      for (int i = 0; i < organisaties.Count; i++)
      {
        if (organisaties[i].GemonitordItemId == 5)
        {
          grafiek3Itemhistorieken = organisaties[i].ItemHistorieken;
        }

      }
      


      //for (int i = grafiek3Itemhistorieken.Count - 7; i < grafiek3Itemhistorieken.Count; i++)
      //{
      //  grafiek3XLabels.Add(grafiek3Itemhistorieken[i].HistoriekDatum.ToShortDateString());
      //  grafiek3Waarden.Add(grafiek3Itemhistorieken[i].AantalVermeldingen);
      //}
      for (int i = 0; i < grafiek3Itemhistorieken.Count; i++)
      {
        grafiek3XLabels.Add(grafiek3Itemhistorieken[i].HistoriekDatum.ToShortDateString());
        grafiek3Waarden.Add(grafiek3Itemhistorieken[i].AantalVermeldingen);
      }




      List<dynamic> grafiek4XLabels = new List<dynamic>();
      List<ItemHistoriek> grafiek4Itemhistorieken = new List<ItemHistoriek>();
      List<double> grafiek4Waarden = new List<double>();

      items = itemManager.GetGemonitordeItems(1).ToList();

      List<string> grafiek4Legendelijst = new List<string>();


      List<ItemHistoriek> itemhistoriekItem1 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem2 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem3 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem4 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem5 = new List<ItemHistoriek>();

      List<double> waardenItem1 = new List<double>();
      List<double> waardenItem2 = new List<double>();
      List<double> waardenItem3 = new List<double>();
      List<double> waardenItem4 = new List<double>();
      List<double> waardenItem5 = new List<double>();

      List<List<double>> alleWaarden = new List<List<double>>();

      //List<DateTime> dagenItems = new List<DateTime>();


      foreach (var element in items)
      {
        if (element.GemonitordItemId == 2)
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          grafiek4Legendelijst.Add(element.Naam);
        }
        if (element.GemonitordItemId == 3)
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          grafiek4Legendelijst.Add(element.Naam);
        }
        if (element.GemonitordItemId == 4)
        {
          itemhistoriekItem3 = element.ItemHistorieken;
          grafiek4Legendelijst.Add(element.Naam);
        }
        if (element.GemonitordItemId == 6)
        {
          itemhistoriekItem4 = element.ItemHistorieken;
          grafiek4Legendelijst.Add(element.Naam);
        }
        if (element.GemonitordItemId == 7)
        {
          itemhistoriekItem5 = element.ItemHistorieken;
          grafiek4Legendelijst.Add(element.Naam);
        }

      }

      //for (int i = itemhistoriekItem1.Count - 5; i < itemhistoriekItem1.Count; i++)
      //{
      //  grafiek4XLabels.Add(itemhistoriekItem1[i].HistoriekDatum.ToShortDateString());

      //  waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
      //  waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
      //  waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);
      //  waardenItem4.Add(itemhistoriekItem4[i].AantalVermeldingen);
      //  waardenItem5.Add(itemhistoriekItem5[i].AantalVermeldingen);

      //  //waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
      //  //waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
      //  //waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);
      //  //waardenItem4.Add(itemhistoriekItem4[i].GemPolariteit);
      //  //waardenItem5.Add(itemhistoriekItem5[i].GemPolariteit);
      //  //waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
      //  //waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
      //  //waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);
      //  //waardenItem4.Add(itemhistoriekItem4[i].GemObjectiviteit);
      //  //waardenItem5.Add(itemhistoriekItem5[i].GemObjectiviteit);
      //}
      for (int i = 0; i < itemhistoriekItem1.Count; i++)
      {
        grafiek4XLabels.Add(itemhistoriekItem1[i].HistoriekDatum.ToShortDateString());

        waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
        waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
        waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);
        waardenItem4.Add(itemhistoriekItem4[i].AantalVermeldingen);
        waardenItem5.Add(itemhistoriekItem5[i].AantalVermeldingen);

        //waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
        //waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
        //waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);
        //waardenItem4.Add(itemhistoriekItem4[i].GemPolariteit);
        //waardenItem5.Add(itemhistoriekItem5[i].GemPolariteit);
        //waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
        //waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
        //waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);
        //waardenItem4.Add(itemhistoriekItem4[i].GemObjectiviteit);
        //waardenItem5.Add(itemhistoriekItem5[i].GemObjectiviteit);
      }




      alleWaarden.Add(waardenItem1);
      alleWaarden.Add(waardenItem2);
      alleWaarden.Add(waardenItem3);
      alleWaarden.Add(waardenItem4);
      alleWaarden.Add(waardenItem5);



      List<dynamic> grafieken = new List<dynamic>()
      {

      new Grafiek()
      {
        GrafiekId = 1,

        Titel = "Vergelijking op moment",
        ToonLegende = false,
        ToonXAs = true,
        ToonYAs = true,

        Type = "bar",

        XOorsprongNul     = true,
        XTitel            = "Items",
        YOorsprongNul     = true,
        YTitel            = "Waarden",
        XLabels = grafiek1XLabels,
        Datawaarden = new List<List<double>>(){ grafiek1Datawaarden},

        Achtergrondkleur = new List<List<string>>(){ new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#ffff00", "#ffa500" }, null, null, null, null},
        Randkleur = new List<List<string>>(){ new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#ffff00", "#ffa500" }, null, null, null, null},
        LegendeLijst = new List<string>{ null, null, null, null, null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = false


      },
      new Grafiek()
      {
        GrafiekId = 2,

        Titel = "aantal tweets",
        ToonLegende = false,
        ToonXAs = true,
        ToonYAs = true,

        Type = "line",

        XOorsprongNul     = true,
        XTitel            = "Items",
        YOorsprongNul     = true,
        YTitel            = "Waarden",
        XLabels = grafiek2XLabels,

        Datawaarden = new List<List<double>>(){ grafiek2Waarden},

        Achtergrondkleur = new List<List<string>>(){ new List<string> { "#3e95cd" }, null, null, null, null},
        Randkleur = new List<List<string>>(){ new List<string> { "#3e95cd" }, null, null, null, null},
        LegendeLijst = new List<string>{ null, null, null, null, null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = false

      },

       new Grafiek()
      {
        GrafiekId = 3,

        Titel = "aantal tweets",
        ToonLegende = false,
        ToonXAs = true,
        ToonYAs = true,

        Type = "line",

        XOorsprongNul     = true,
        XTitel            = "Items",
        YOorsprongNul     = true,
        YTitel            = "Waarden",
        XLabels = grafiek3XLabels,

        Datawaarden = new List<List<double>>(){ grafiek3Waarden},

        Achtergrondkleur = new List<List<string>>(){ new List<string> { "#3e95cd" }, null, null, null, null},
        Randkleur = new List<List<string>>(){ new List<string> { "#3e95cd" }, null, null, null, null},
        LegendeLijst = new List<string>{ null, null, null, null, null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = false

      },
       new Grafiek()
      {
        GrafiekId = 4,

        Titel = "aantal tweets",
        ToonLegende = true,
        ToonXAs = true,
        ToonYAs = true,

        Type = "line",

        XOorsprongNul     = true,
        XTitel            = "Items",
        YOorsprongNul     = true,
        YTitel            = "Waarden",
        XLabels = grafiek4XLabels,

        Datawaarden = alleWaarden,

        Achtergrondkleur = new List<List<string>>(){ new List<string> { "#3e95cd" }, new List<string> {"#8e5ea2" }, new List<string> { "#3cba9f"}, new List<string> { "#e8c3b9"}, new List<string> { "#c45850" } },
        Randkleur        = new List<List<string>>(){ new List<string> { "#3e95cd" }, new List<string> {"#8e5ea2" }, new List<string> { "#3cba9f"}, new List<string> { "#e8c3b9"}, new List<string> { "#c45850" } },
        LegendeLijst     = new List<string>{ grafiek4Legendelijst[0], grafiek4Legendelijst[1], grafiek4Legendelijst[2], grafiek4Legendelijst[3], grafiek4Legendelijst[4] },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = true

      }


    };

      ViewBag.Grafieken = grafieken;



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

    public virtual ActionResult LaadGrafiekToevoegen()
    {

      return PartialView("~/Views/Shared/Dashboard/Grafieken/GrafiekenToevoegen.cshtml");

    }




    //public virtual ActionResult LaadTop5(string item)
    //{
    //  items = itemManager.GetGemonitordeItems(1).ToList();

    //  foreach (var element in items)
    //  {
    //    if (element.Naam.Equals(item))
    //    {
    //      ViewBag.ItemNaam = element.Naam;
    //      //ViewBag.ItemTop5 = element.
    //    }
    //  }

    //  return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml", ViewBag);
    //}

    //public virtual ActionResult LaadTop10(string item)
    //{
    //  items = itemManager.GetGemonitordeItems(1).ToList();

    //  foreach (var element in items)
    //  {
    //    if (element.Naam.Equals(item))
    //    {
    //      ViewBag.ItemNaam = element.Naam;
    //      //ViewBag.ItemTop10 = element.
    //    }
    //  }

    //  return PartialView("~/Views/Shared/GetalEnOverzicht/Top10.cshtml", ViewBag);
    //}







    public void LaadThema(List<Thema> items)
    {
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }
    }

    public virtual ActionResult LaadOrganisaties1Item()
    {

      var items = itemManager.GetOrganisaties(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties1Item.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties2Items()
    {
      var items = itemManager.GetOrganisaties(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties2Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties3Items()
    {
      var items = itemManager.GetOrganisaties(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties3Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties4Items()
    {
      var items = itemManager.GetOrganisaties(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties4Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties5Items()
    {
      var items = itemManager.GetOrganisaties(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties5Items.cshtml", ViewBag);
    }


    public virtual ActionResult LaadPersonen1Item()
    {
      var items = itemManager.GetPersonen(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.PersonenViewbag = selects;


      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen1Item.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen2Items()
    {
      var items = itemManager.GetPersonen(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.PersonenViewbag = selects;


      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen2Items.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen3Items()
    {
      var items = itemManager.GetPersonen(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.PersonenViewbag = selects;


      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen3Items.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen4Items()
    {
      var items = itemManager.GetPersonen(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.PersonenViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen4Items.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen5Items()
    {
      var items = itemManager.GetPersonen(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.PersonenViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen5Items.cshtml", ViewBag);

    }


    public virtual ActionResult LaadThemas1Item()
    {
      var items = itemManager.GetThemas(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.ThemasViewbag = selects;


      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas1Item.cshtml", ViewBag);
    }

    public virtual ActionResult LaadThemas2Items()
    {
      var items = itemManager.GetThemas(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.ThemasViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas2Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadThemas3Items()
    {
      var items = itemManager.GetThemas(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.ThemasViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas3Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadThemas4Items()
    {
      var items = itemManager.GetThemas(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.ThemasViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas4Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadThemas5Items()
    {
      var items = itemManager.GetThemas(1).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.ThemasViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas5Items.cshtml", ViewBag);
    }



    //public virtual ActionResult LaadStatistiekenKeuze()
    //{
    //  return PartialView("~/Views/Shared/Dashboard/Statistieken/StatistiekenKeuze.cshtml");
    //}

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


    //public virtual ActionResult LaadGrafiekenKeuze()
    //{
    //  return PartialView("~/Views/Shared/Dashboard/Grafieken/GrafiekenKeuze.cshtml");
    //}

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

    public virtual ActionResult LaadVergelijkingOpMoment2Items(string grafiektitel, string item1, string item2, string gewensteData)
    {
      items = itemManager.GetGemonitordeItems(1).ToList();

      List<string> xLabels = new List<string>();
      List<double> data = new List<double>();

      switch (gewensteData)
      {
        case "av":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            
          }


          break;
        case "gp":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
           
          }

          break;
        case "go":

          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            
          }

          break;

      }


      ViewBag.XLabels = xLabels;
      ViewBag.Data = data;
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram1Dataset.cshtml", ViewBag);

    }

    public virtual ActionResult LaadVergelijkingOpMoment3Items(string grafiektitel, string item1, string item2, string item3, string gewensteData)
    {
      items = itemManager.GetGemonitordeItems(1).ToList();

      List<string> xLabels = new List<string>();
      List<double> data = new List<double>();

      switch (gewensteData)
      {
        case "av":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            
          }


          break;
        case "gp":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            
          }

          break;
        case "go":

          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            
          }

          break;

      }


      ViewBag.XLabels = xLabels;
      ViewBag.Data = data;
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram1Dataset.cshtml", ViewBag);

    }

    public virtual ActionResult LaadVergelijkingOpMoment4Items(string grafiektitel, string item1, string item2, string item3, string item4, string gewensteData)
    {
      items = itemManager.GetGemonitordeItems(1).ToList();

      List<string> xLabels = new List<string>();
      List<double> data = new List<double>();

      switch (gewensteData)
      {
        case "av":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            
          }


          break;
        case "gp":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
           
          }

          break;
        case "go":

          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            
          }

          break;

      }


      ViewBag.XLabels = xLabels;
      ViewBag.Data = data;
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram1Dataset.cshtml", ViewBag);

    }




    public virtual ActionResult LaadVergelijkingOpMoment5Items(string grafiektitel, string item1, string item2, string item3, string item4, string item5, string gewensteData)
    {
      items = itemManager.GetGemonitordeItems(1).ToList();

      List<string> xLabels = new List<string>();
      List<double> data = new List<double>();

      switch (gewensteData)
      {
        case "av":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
            if (element.Naam.Equals(item5))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);
            }
          }


          break;
        case "gp":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
            if (element.Naam.Equals(item5))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);
            }
          }

          break;
        case "go":

          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
            if (element.Naam.Equals(item5))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);
            }
          }

          break;

      }


      ViewBag.XLabels = xLabels;
      ViewBag.Data = data;
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram1Dataset.cshtml", ViewBag);

    }


    //public virtual ActionResult LaadLijndiagramAantalTweets(string grafiektitel, string item)
    //{

    //  var gemonitordeItems = itemManager.GetGemonitordeItems(1).ToList();

    //  foreach (var element in gemonitordeItems)
    //  {
    //    if (element.Naam.Equals(item))
    //    {
    //      ViewBag.ItemDagen = 
    //      ViewBag.ItemAantalTweets = 

    //    }

    //  }

    //  ViewBag.Grafiektitel = grafiektitel;

    //  return PartialView("~/Views/Shared/Grafieken/Lijndiagram/LijndiagramAantalTweets.cshtml", ViewBag);
    //}


    public virtual ActionResult LaadLijndiagramAantalTweets(string grafiektitel, string item, string aantalDagen)
    {
      //int dagen = Int32.Parse(aantalDagen);
      List<ItemHistoriek> itemhistorieken = new List<ItemHistoriek>();
      List<dynamic> xLabels = new List<dynamic>();
      List<double> waarden = new List<double>();

      List<GemonitordItem> gemonitordeItems = itemManager.GetGemonitordeItems(1).ToList();

      for (int i = 0; i < gemonitordeItems.Count; i++)
      {
        if (gemonitordeItems[i].Naam.Equals(item))
        {
          itemhistorieken = gemonitordeItems[i].ItemHistorieken;

        }
      }

      //for (int i = itemhistorieken.Count - 5; i < itemhistorieken.Count; i++)
      //{
      //  xLabels.Add(itemhistorieken[i].HistoriekDatum.ToShortDateString());
      //  waarden.Add(itemhistorieken[i].AantalVermeldingen);
      //}
      for (int i = 0; i < itemhistorieken.Count; i++)
      {
        xLabels.Add(itemhistorieken[i].HistoriekDatum.ToShortDateString());
        waarden.Add(itemhistorieken[i].AantalVermeldingen);
      }

      ViewBag.ItemDagen = xLabels;
      ViewBag.ItemAantalTweets = waarden;
      

      ViewBag.Grafiektitel = grafiektitel;


      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/LijndiagramAantalTweets.cshtml", ViewBag);
    }







    public virtual ActionResult LaadVergelijkingDoorheenTijd2Items(string grafiektitel, string item1, string item2, string aantalDagen, string gewensteData)
    {

      //int dagen = Int32.Parse(aantalDagen);

      items = itemManager.GetGemonitordeItems(1).ToList();

      List<string> legendelijst = new List<string>();

      List<ItemHistoriek> itemhistoriekItem1 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem2 = new List<ItemHistoriek>();


      List<double> waardenItem1 = new List<double>();
      List<double> waardenItem2 = new List<double>();


      List<List<double>> alleWaarden = new List<List<double>>();

      List<DateTime> dagenItems = new List<DateTime>();



      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
        if (element.Naam.Equals(item2))
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }

      }

      //for (int i = itemhistoriekItem1.Count - dagen; i < itemhistoriekItem1.Count; i++)
      //{
      //  dagenItems.Add(itemhistoriekItem1[i].HistoriekDatum);

      //  switch (gewensteData)
      //  {
      //    case "av":
      //      waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
      //      waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);


      //      break;
      //    case "gp":
      //      waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
      //      waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);

      //      break;
      //    case "go":
      //      waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
      //      waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);

      //      break;
      //  }
      //}

      for (int i = 0; i < itemhistoriekItem1.Count; i++)
      {
        dagenItems.Add(itemhistoriekItem1[i].HistoriekDatum);

        switch (gewensteData)
        {
          case "av":
            waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
            waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);


            break;
          case "gp":
            waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);

            break;
          case "go":
            waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);

            break;
        }
      }

      alleWaarden.Add(waardenItem1);
      alleWaarden.Add(waardenItem2);


      ViewBag.Waarden = alleWaarden;
      ViewBag.Legendelijst = legendelijst;
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram2Items.cshtml", ViewBag);

    }


    public virtual ActionResult LaadVergelijkingDoorheenTijd3Items(string grafiektitel, string item1, string item2, string item3, string aantalDagen, string gewensteData)
    {

      //int dagen = Int32.Parse(aantalDagen);

      items = itemManager.GetGemonitordeItems(1).ToList();

      List<string> legendelijst = new List<string>();

      List<ItemHistoriek> itemhistoriekItem1 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem2 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem3 = new List<ItemHistoriek>();


      List<double> waardenItem1 = new List<double>();
      List<double> waardenItem2 = new List<double>();
      List<double> waardenItem3 = new List<double>();

      List<List<double>> alleWaarden = new List<List<double>>();

      List<DateTime> dagenItems = new List<DateTime>();



      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
        if (element.Naam.Equals(item2))
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
        if (element.Naam.Equals(item3))
        {
          itemhistoriekItem3 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }

      }

      //for (int i = itemhistoriekItem1.Count - dagen; i < itemhistoriekItem1.Count; i++)
      //{
      //  dagenItems.Add(itemhistoriekItem1[i].HistoriekDatum);

      //  switch (gewensteData)
      //  {
      //    case "av":
      //      waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
      //      waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
      //      waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);

      //      break;
      //    case "gp":
      //      waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
      //      waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
      //      waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);

      //      break;
      //    case "go":
      //      waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
      //      waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
      //      waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);

      //      break;
      //  }
      //}

      for (int i = 0; i < itemhistoriekItem1.Count; i++)
      {
        dagenItems.Add(itemhistoriekItem1[i].HistoriekDatum);

        switch (gewensteData)
        {
          case "av":
            waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
            waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
            waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);

            break;
          case "gp":
            waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);

            break;
          case "go":
            waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);

            break;
        }
      }

        alleWaarden.Add(waardenItem1);
      alleWaarden.Add(waardenItem2);
      alleWaarden.Add(waardenItem3);

      ViewBag.Waarden = alleWaarden;
      ViewBag.Legendelijst = legendelijst;
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram3Items.cshtml", ViewBag);

    }



    public virtual ActionResult LaadVergelijkingDoorheenTijd4Items(string grafiektitel, string item1, string item2, string item3, string item4, string aantalDagen, string gewensteData)
    {

      //int dagen = Int32.Parse(aantalDagen);

      items = itemManager.GetGemonitordeItems(1).ToList();

      List<string> legendelijst = new List<string>();

      List<ItemHistoriek> itemhistoriekItem1 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem2 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem3 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem4 = new List<ItemHistoriek>();

      List<double> waardenItem1 = new List<double>();
      List<double> waardenItem2 = new List<double>();
      List<double> waardenItem3 = new List<double>();
      List<double> waardenItem4 = new List<double>();

      List<List<double>> alleWaarden = new List<List<double>>();

      List<DateTime> dagenItems = new List<DateTime>();



      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
        if (element.Naam.Equals(item2))
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
        if (element.Naam.Equals(item3))
        {
          itemhistoriekItem3 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
        if (element.Naam.Equals(item4))
        {
          itemhistoriekItem4 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }

      }

      //for (int i = itemhistoriekItem1.Count - dagen; i < itemhistoriekItem1.Count; i++)
      //{
      //  dagenItems.Add(itemhistoriekItem1[i].HistoriekDatum);

      //  switch (gewensteData)
      //  {
      //    case "av":
      //      waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
      //      waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
      //      waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);
      //      waardenItem4.Add(itemhistoriekItem4[i].AantalVermeldingen);

      //      break;
      //    case "gp":
      //      waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
      //      waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
      //      waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);
      //      waardenItem4.Add(itemhistoriekItem4[i].GemPolariteit);
      //      break;
      //    case "go":
      //      waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
      //      waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
      //      waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);
      //      waardenItem4.Add(itemhistoriekItem4[i].GemObjectiviteit);
      //      break;
      //  }
      //}

      for (int i = 0; i < itemhistoriekItem1.Count; i++)
      {
        dagenItems.Add(itemhistoriekItem1[i].HistoriekDatum);

        switch (gewensteData)
        {
          case "av":
            waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
            waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
            waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);
            waardenItem4.Add(itemhistoriekItem4[i].AantalVermeldingen);

            break;
          case "gp":
            waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);
            waardenItem4.Add(itemhistoriekItem4[i].GemPolariteit);
            break;
          case "go":
            waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);
            waardenItem4.Add(itemhistoriekItem4[i].GemObjectiviteit);
            break;
        }
      }

      alleWaarden.Add(waardenItem1);
      alleWaarden.Add(waardenItem2);
      alleWaarden.Add(waardenItem3);
      alleWaarden.Add(waardenItem4);

      ViewBag.Waarden = alleWaarden;
      ViewBag.Legendelijst = legendelijst;
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram4Items.cshtml", ViewBag);

    }

    public virtual ActionResult LaadVergelijkingDoorheenTijd5Items(string grafiektitel, string item1, string item2, string item3, string item4, string item5, string aantalDagen, string gewensteData)
    {

      //int dagen = Int32.Parse(aantalDagen);

      items = itemManager.GetGemonitordeItems(1).ToList();

      List<string> legendelijst = new List<string>();

      List<ItemHistoriek> itemhistoriekItem1 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem2 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem3 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem4 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem5 = new List<ItemHistoriek>();

      List<double> waardenItem1 = new List<double>();
      List<double> waardenItem2 = new List<double>();
      List<double> waardenItem3 = new List<double>();
      List<double> waardenItem4 = new List<double>();
      List<double> waardenItem5 = new List<double>();

      List<List<double>> alleWaarden = new List<List<double>>();

      List<string> dagenItems = new List<string>();



      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
        if (element.Naam.Equals(item2))
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
        if (element.Naam.Equals(item3))
        {
          itemhistoriekItem3 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
        if (element.Naam.Equals(item4))
        {
          itemhistoriekItem4 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
        if (element.Naam.Equals(item5))
        {
          itemhistoriekItem5 = element.ItemHistorieken;
          legendelijst.Add(element.Naam);
        }
      }

      //for (int i = itemhistoriekItem1.Count - dagen; i < itemhistoriekItem1.Count; i++)
      //{
      //  dagenItems.Add(itemhistoriekItem1[i].HistoriekDatum);

      //  switch (gewensteData)
      //  {
      //    case "av":
      //      waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
      //      waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
      //      waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);
      //      waardenItem4.Add(itemhistoriekItem4[i].AantalVermeldingen);
      //      waardenItem5.Add(itemhistoriekItem5[i].AantalVermeldingen);

      //      break;
      //    case "gp":
      //      waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
      //      waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
      //      waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);
      //      waardenItem4.Add(itemhistoriekItem4[i].GemPolariteit);
      //      waardenItem5.Add(itemhistoriekItem5[i].GemPolariteit);
      //      break;
      //    case "go":
      //      waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
      //      waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
      //      waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);
      //      waardenItem4.Add(itemhistoriekItem4[i].GemObjectiviteit);
      //      waardenItem5.Add(itemhistoriekItem5[i].GemObjectiviteit);
      //      break;
      //  }
      //}

      for (int i = 0; i < itemhistoriekItem1.Count; i++)
      {
        dagenItems.Add(itemhistoriekItem1[i].HistoriekDatum);

        switch (gewensteData)
        {
          case "av":
            waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
            waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
            waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);
            waardenItem4.Add(itemhistoriekItem4[i].AantalVermeldingen);
            waardenItem5.Add(itemhistoriekItem5[i].AantalVermeldingen);

            break;
          case "gp":
            waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);
            waardenItem4.Add(itemhistoriekItem4[i].GemPolariteit);
            waardenItem5.Add(itemhistoriekItem5[i].GemPolariteit);
            break;
          case "go":
            waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);
            waardenItem4.Add(itemhistoriekItem4[i].GemObjectiviteit);
            waardenItem5.Add(itemhistoriekItem5[i].GemObjectiviteit);
            break;
        }
      }



      alleWaarden.Add(waardenItem1);
      alleWaarden.Add(waardenItem2);
      alleWaarden.Add(waardenItem3);
      alleWaarden.Add(waardenItem4);
      alleWaarden.Add(waardenItem5);

      ViewBag.Data = alleWaarden;
      ViewBag.Legendelijst = legendelijst;
      ViewBag.Grafiektitel = grafiektitel;
      ViewBag.XLabels = dagenItems;
      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram5Items.cshtml", ViewBag);

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
        case 1: grafiekKeuze = GrafiekKeuze.KruisingTaart; break;
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

    public ActionResult UpdateGrafiekEnUpdateDashboard(int grafiekId, int deelplatformId, string titel,
      int periode, bool toonLegende, bool toonXAs, bool toonYAs, int keuze, string xTitel, string yTitel,
      bool xOnder, bool xOorsprongNul, bool yOorsprongNul, int dashboardId,
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

      GrafiekKeuze grafiekKeuze = GrafiekKeuze.EvolutieAantalVermeldingen1Item;
      switch (keuze)
      {
        case 1: grafiekKeuze = GrafiekKeuze.KruisingTaart; break;
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
        GrafiekId = grafiekId,
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
      textgainController.HaalBerichtenOp(deelplatformenManager.GetDeelplatform(id));
    }
  }
}
