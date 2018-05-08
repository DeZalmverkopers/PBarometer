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
      items = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item))
        {
          ViewBag.ItemNaam = element.Naam;
          ViewBag.ItemVermeldingen = element.TotaalAantalVermeldingen;
        }
      }

      return PartialView("~/Views/Shared/GetalEnOverzicht/Getal.cshtml", ViewBag);
    }

    public virtual ActionResult LaadGetalTrend(string item)
    {
      items = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item))
        {
          ViewBag.ItemNaam = element.Naam;
          ViewBag.ItemVermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.ItemTrend = element.VermeldingenTrend;

        }
      }

      return PartialView("~/Views/Shared/GetalEnOverzicht/GetalTrend.cshtml", ViewBag);
    }


    public virtual ActionResult LaadTop5(string item)
    {
      ViewBag.Item = item;

      return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml", ViewBag);
    }

    public virtual ActionResult LaadTop10(string item)
    {
      ViewBag.Item = item;

      return PartialView("~/Views/Shared/GetalEnOverzicht/Top10.cshtml", ViewBag);
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


    //public virtual ActionResult LaadVergelijkingOpMoment1Item(string item1)
    //{

    //  ViewBag.Item1Naam = item1;
    //  ViewBag.Item1Vermeldingen = 15;


    //  return PartialView("~/Views/Shared/Grafieken/Staafdiagram.cshtml", ViewBag);
    //}


    public virtual ActionResult LaadVergelijkingOpMoment1Item(string grafiektitel, string item1)
    {

      var gemonitordeItems = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in gemonitordeItems)
      {
        if (element.Naam.Equals(item1))
        {
          ViewBag.Item1Naam = element.Naam;
          //ViewBag.Item1Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item1Vermeldingen = 15;

        }

      }

      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram1Item.cshtml", ViewBag);
    }



    public virtual ActionResult LaadVergelijkingOpMoment2Items(string grafiektitel, string item1, string item2)
    {

      items = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          ViewBag.Item1Naam = element.Naam;
          //ViewBag.Item1Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item1Vermeldingen = 5;

        }
        if (element.Naam.Equals(item2))
        {
          ViewBag.Item2Naam = element.Naam;
          //ViewBag.Item2Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item2Vermeldingen = 10;

        }
      }

      ViewBag.Grafiektitel = grafiektitel;


      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram2Items.cshtml", ViewBag);

    }


    public virtual ActionResult LaadVergelijkingOpMoment3Items(string grafiektitel, string item1, string item2, string item3)
    {

      items = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          ViewBag.Item1Naam = element.Naam;
          //ViewBag.Item1Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item1Vermeldingen = 5;

        }
        if (element.Naam.Equals(item2))
        {
          ViewBag.Item2Naam = element.Naam;
          //ViewBag.Item2Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item2Vermeldingen = 10;

        }
        if (element.Naam.Equals(item3))
        {
          ViewBag.Item3Naam = element.Naam;
          ////ViewBag.Item3Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item3Vermeldingen = 15;

        }
      }

      ViewBag.Grafiektitel = grafiektitel;


      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram3Items.cshtml", ViewBag);

    }


    public virtual ActionResult LaadVergelijkingOpMoment4Items(string grafiektitel, string item1, string item2, string item3, string item4)
    {

      items = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          ViewBag.Item1Naam = element.Naam;
          //ViewBag.Item1Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item1Vermeldingen = 5;

        }
        if (element.Naam.Equals(item2))
        {
          ViewBag.Item2Naam = element.Naam;
          //ViewBag.Item2Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item2Vermeldingen = 10;

        }
        if (element.Naam.Equals(item3))
        {
          ViewBag.Item3Naam = element.Naam;
          //ViewBag.Item3Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item3Vermeldingen = 15;

        }
        if (element.Naam.Equals(item4))
        {
          ViewBag.Item4Naam = element.Naam;
          //ViewBag.Item4Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item4Vermeldingen = 20;

        }
      }

      ViewBag.Grafiektitel = grafiektitel;


      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram4Items.cshtml", ViewBag);
    }



    public virtual ActionResult LaadVergelijkingOpMoment5Items(string grafiektitel, string item1, string item2, string item3, string item4, string item5)
    {

      items = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          ViewBag.Item1Naam = element.Naam;
          //ViewBag.Item1Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item1Vermeldingen = 5;

        }
        if (element.Naam.Equals(item2))
        {
          ViewBag.Item2Naam = element.Naam;
          //ViewBag.Item2Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item2Vermeldingen = 10;

        }
        if (element.Naam.Equals(item3))
        {
          ViewBag.Item3Naam = element.Naam;
          //ViewBag.Item3Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item3Vermeldingen = 15;

        }
        if (element.Naam.Equals(item4))
        {
          ViewBag.Item4Naam = element.Naam;
          //ViewBag.Item4Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item4Vermeldingen = 20;

        }
        if (element.Naam.Equals(item5))
        {
          ViewBag.Item5Naam = element.Naam;
          //ViewBag.Item5Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item5Vermeldingen = 25;

        }
      }

      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Staafdiagram/Staafdiagram5Items.cshtml", ViewBag);

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


    public virtual ActionResult LaadLijndiagramAantalTweets(string grafiektitel)
    {

      //var gemonitordeItems = itemManager.GetGemonitordeItems(1).ToList();

      //foreach (var element in gemonitordeItems)
      //{
      //  if (element.Naam.Equals(item))
      //  {
      //    ViewBag.ItemAantalTweets = element.ItemHistorieken;

      //  }

      //}

      ViewBag.ItemDagen = new string[] { "Dag 1", "Dag 2", "Dag 3", "Dag 4", "Dag 5" };
      ViewBag.ItemAantalTweets = new int[] { 5, 20, 15, 25, 10 };
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/LijndiagramAantalTweets.cshtml", ViewBag);
    }

    public virtual ActionResult LaadVergelijkingDoorheenTijd1Item(string grafiektitel, string item1)
    {
      var gemonitordeItems = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in gemonitordeItems)
      {
        if (element.Naam.Equals(item1))
        {
          ViewBag.Item1Naam = element.Naam;

        }

      }

      ViewBag.Item1Data = new int[] { 5, 20, 15, 25, 10 };

      ViewBag.ItemDagen = new string[] { "Dag 1", "Dag 2", "Dag 3", "Dag 4", "Dag 5" };
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram1Item.cshtml", ViewBag);
    }

    //public virtual ActionResult LaadVergelijkingDoorheenTijd2Items(string grafiektitel, string item1, string item2)
    //{

    //}

    public virtual ActionResult LaadVergelijkingDoorheenTijd2Items(string grafiektitel, string item1, string item2)
    {
      items = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          ViewBag.Item1Naam = element.Naam;
          //ViewBag.Item1Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item1Data = new int[] { 28, 48, 40, 19, 86 };

        }
        if (element.Naam.Equals(item2))
        {
          ViewBag.Item2Naam = element.Naam;
          //ViewBag.Item2Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item2Data = new int[] { 65, 59, 80, 81, 56 };

        }
      }

      ViewBag.ItemDagen = new string[] { "Dag 1", "Dag 2", "Dag 3", "Dag 4", "Dag 5" };
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram2Items.cshtml", ViewBag);
    }


    public virtual ActionResult LaadVergelijkingDoorheenTijd3Items(string grafiektitel, string item1, string item2, string item3)
    {
      items = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          ViewBag.Item1Naam = element.Naam;
          //ViewBag.Item1Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item1Data = new int[] { 28, 48, 40, 19, 86 };

        }
        if (element.Naam.Equals(item2))
        {
          ViewBag.Item2Naam = element.Naam;
          //ViewBag.Item2Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item2Data = new int[] { 65, 59, 80, 81, 56 };

        }
        if (element.Naam.Equals(item3))
        {
          ViewBag.Item3Naam = element.Naam;
          //ViewBag.Item3Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item3Data = new int[] { 34, 88, 77, 66, 55 };

        }
      }

      ViewBag.ItemDagen = new string[] { "Dag 1", "Dag 2", "Dag 3", "Dag 4", "Dag 5" };
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram3Items.cshtml", ViewBag);
    }


    //public virtual ActionResult LaadVergelijkingDoorheenTijd3Items(string grafiektitel, string item1, string item2, string item3)
    //{

    //}

    public virtual ActionResult LaadVergelijkingDoorheenTijd4Items(string grafiektitel, string item1, string item2, string item3, string item4)
    {
      items = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          ViewBag.Item1Naam = element.Naam;
          //ViewBag.Item1Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item1Data = new int[] { 28, 48, 40, 19, 86 };

        }
        if (element.Naam.Equals(item2))
        {
          ViewBag.Item2Naam = element.Naam;
          //ViewBag.Item2Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item2Data = new int[] { 65, 59, 80, 81, 56 };

        }
        if (element.Naam.Equals(item3))
        {
          ViewBag.Item3Naam = element.Naam;
          //ViewBag.Item3Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item3Data = new int[] { 34, 88, 77, 66, 55 };

        }
        if (element.Naam.Equals(item4))
        {
          ViewBag.Item4Naam = element.Naam;
          //ViewBag.Item4Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item4Data = new int[] { 20, 45, 65, 34, 22 };

        }
      }

      ViewBag.ItemDagen = new string[] { "Dag 1", "Dag 2", "Dag 3", "Dag 4", "Dag 5" };
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram4Items.cshtml", ViewBag);
    }



    //public virtual ActionResult LaadVergelijkingDoorheenTijd4Items(string grafiektitel, string item1, string item2, string item3, string item4)
    //{

    //}

    public virtual ActionResult LaadVergelijkingDoorheenTijd5Items(string grafiektitel, string item1, string item2, string item3, string item4, string item5)
    {
      items = itemManager.GetGemonitordeItems(1).ToList();

      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          ViewBag.Item1Naam = element.Naam;
          //ViewBag.Item1Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item1Data = new int[] { 28, 48, 40, 19, 86 };

        }
        if (element.Naam.Equals(item2))
        {
          ViewBag.Item2Naam = element.Naam;
          //ViewBag.Item2Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item2Data = new int[] { 65, 59, 80, 81, 56 };

        }
        if (element.Naam.Equals(item3))
        {
          ViewBag.Item3Naam = element.Naam;
          //ViewBag.Item3Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item3Data = new int[] { 34, 88, 77, 66, 55 };

        }
        if (element.Naam.Equals(item4))
        {
          ViewBag.Item4Naam = element.Naam;
          //ViewBag.Item4Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item4Data = new int[] { 20, 45, 65, 34, 22 };

        }
        if (element.Naam.Equals(item5))
        {
          ViewBag.Item5Naam = element.Naam;
          //ViewBag.Item5Vermeldingen = element.TotaalAantalVermeldingen;
          ViewBag.Item5Data = new int[] { 55, 66, 77, 88, 34 };

        }
      }

      ViewBag.ItemDagen = new string[] { "Dag 1", "Dag 2", "Dag 3", "Dag 4", "Dag 5" };
      ViewBag.Grafiektitel = grafiektitel;

      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram5Items.cshtml", ViewBag);
    }

    //public virtual ActionResult LaadVergelijkingDoorheenTijd5Items(string grafiektitel, string item1, string item2, string item3, string item4, string item5)
    //{
    //  items = itemManager.GetGemonitordeItems(1).ToList();

    //  foreach (var element in items)
    //  {
    //    if (element.Naam.Equals(item1))
    //    {
    //      ViewBag.Item1Naam = element.Naam;
    //      //ViewBag.Item1Vermeldingen = element.TotaalAantalVermeldingen;
    //      ViewBag.Item1Data = 

    //    }
    //    if (element.Naam.Equals(item2))
    //    {
    //      ViewBag.Item2Naam = element.Naam;
    //      //ViewBag.Item2Vermeldingen = element.TotaalAantalVermeldingen;
    //      ViewBag.Item2Data = 

    //    }
    //    if (element.Naam.Equals(item3))
    //    {
    //      ViewBag.Item3Naam = element.Naam;
    //      //ViewBag.Item3Vermeldingen = element.TotaalAantalVermeldingen;
    //      ViewBag.Item3Data = 

    //    }
    //    if (element.Naam.Equals(item4))
    //    {
    //      ViewBag.Item4Naam = element.Naam;
    //      //ViewBag.Item4Vermeldingen = element.TotaalAantalVermeldingen;
    //      ViewBag.Item4Data = 

    //    }
    //    if (element.Naam.Equals(item5))
    //    {
    //      ViewBag.Item5Naam = element.Naam;
    //      //ViewBag.Item5Vermeldingen = element.TotaalAantalVermeldingen;
    //      ViewBag.Item5Data = 

    //    }
    //  }

    //  ViewBag.ItemDagen = new string[] { "Dag 1", "Dag 2", "Dag 3", "Dag 4", "Dag 5" };
    //  ViewBag.Grafiektitel = grafiektitel;

    //  return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram5Items.cshtml", ViewBag);
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

    public ActionResult VoegGrafiekToeEnUpdateDashboard(int deelplatformId, string titel, int periode, bool toonLegende, bool toonXAs, bool toonYAs, string type)
    {
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
        Titel = titel,
        Periode = periode,
        ToonLegende = toonLegende,
        ToonXAs = toonXAs,
        ToonYAs = toonYAs,
        Type = (GrafiekType) Enum.Parse(typeof(GrafiekType), type, true),
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