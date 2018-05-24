﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using BL.IdentityFramework;
using Domain.Dashboards;
using Domain.Deelplatformen;
using Domain.Gemonitordeitems;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVC.Controllers.Api;

//Seppe

namespace MVC.Controllers
{
  public partial class DashboardController : Controller
  {
    GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
    DashboardsManager dashboardsManager = new DashboardsManager();
    List<SelectListItem> selects;
    List<GemonitordItem> items;
    //List<Thema> themas;
    HomeController homeController = new HomeController();
    GrafiekenManager grafiekenManager = new GrafiekenManager();



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
    // GET: Dashboard



    public virtual ActionResult Index()
    {

      if (HuidigDeelplatform == null)
      {
        return RedirectToAction("Index", "Home");
      }



      ViewBag.DeelplatformNaam = HuidigDeelplatform.Naam;
      ViewBag.Afbeelding = HuidigDeelplatform.AfbeeldingPad ?? "default.png";


      if (HuidigDashboard != null)
      {
        var grafieken = grafiekenManager.GetGrafieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId, true).ToList();

        ViewBag.Grafieken = grafieken;
      }


      return View();
    }

    public virtual ActionResult LaadGrafiekAanpassen(string id)
    {
      int idInt = Int32.Parse(id);

      List<Grafiek> grafieken = grafiekenManager.GetGrafieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId, true).ToList();

      foreach (var grafiek in grafieken)
      {
        if (grafiek.GrafiekId == idInt)
        {
          ViewBag.AanTePassenGrafiek = grafiek;
        }
      }



      return PartialView("~/Views/Shared/Dashboard/GrafiekAanpassen.cshtml", ViewBag);
    }


    public ActionResult GrafiekAanpassen(int id, string titel, string xTitel, string yTitel, string type)
    {

      List<Grafiek> grafieken = grafiekenManager.GetGrafieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId, true).ToList();

      foreach (var grafiek in grafieken)
      {
        if (grafiek.GrafiekId == id)
        {
          grafiek.Titel = titel;
          grafiek.Type = type;
          grafiek.XTitel = xTitel;
          grafiek.YTitel = yTitel;
          grafiekenManager.ChangeGrafiek(grafiek);
        }
      }


      return RedirectToAction("Index");

    }

    public ActionResult GrafiekVerwijderen(string id)
    {
      int idInt = Int32.Parse(id);

      //GrafiekenManager grafiekenManager = new GrafiekenManager();
      List<Grafiek> grafieken = grafiekenManager.GetGrafieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId, true).ToList();



      foreach (var grafiek in grafieken)
      {
        if (grafiek.GrafiekId == idInt)
        {
          grafiekenManager.RemoveGrafiek(grafiek);
        }
      }

      return RedirectToAction("Index");
    }



    public virtual ActionResult LaadLegePartialView()
    {
      return PartialView("~/Views/Shared/LegePartialView.cshtml");
    }

    public virtual ActionResult LaadGrafiekToevoegen()
    {

      return PartialView("~/Views/Shared/Dashboard/Grafieken/GrafiekenToevoegen.cshtml");

    }

    public virtual ActionResult LaadGrafiekenNietIngelogd()
    {

      ViewBag.GrafiekenNietIngelogd = grafiekenManager.GetGrafiekenTest();

      return PartialView("~/Views/Shared/Dashboard/Grafieken/GrafiekenNietIngelogd.cshtml", ViewBag);
    }

    [Authorize]
    [HttpGet]
    public ActionResult VoegGrafiekAantalTweetsToeUitOverzicht(int gemonitordItemId)
    {
      GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
      DashboardsManager dashboardsManager = new DashboardsManager();
      GemonitordItem gemonitordItem = gemonitordeItemsManager.GetGemonitordItem(gemonitordItemId);
      Dashboard dashboard = dashboardsManager.GetDashboardVanGebruikerMetGrafieken(User.Identity.GetUserId(), HuidigDeelplatform.DeelplatformId);

      return RedirectToAction("Index");
    }



    #region organisaties laden

    public virtual ActionResult LaadOrganisaties1Item()
    {

      var items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties5Items.cshtml", ViewBag);
    }

    #endregion


    #region personen laden

    public virtual ActionResult LaadPersonen1Item()
    {
      var items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.PersonenViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen5Items.cshtml", ViewBag);

    }

    #endregion




    #region themas laden
    public virtual ActionResult LaadThemas1Item()
    {
      var items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).ToList();
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
      var items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.ThemasViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas5Items.cshtml", ViewBag);
    }
    #endregion




    #region soorten grafieken laden
    public virtual ActionResult LaadAantalTweets()
    {
      return PartialView("~/Views/Shared/Dashboard/Grafieken/AantalTweets.cshtml");
    }

    public virtual ActionResult LaadItemsKruisen()
    {
      var items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.Naam });
      }

      ViewBag.GemonitordeItemsViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Grafieken/ItemsKruisen.cshtml", ViewBag);
    }

    public virtual ActionResult LaadVergelijkenDoorheenDeTijd()
    {
      return PartialView("~/Views/Shared/Dashboard/Grafieken/VergelijkenDoorheenDeTijd.cshtml");
    }

    public virtual ActionResult LaadVergelijkenOpMoment()
    {
      return PartialView("~/Views/Shared/Dashboard/Grafieken/VergelijkenOpMoment.cshtml");
    }
    #endregion



    #region laad vergelijking op moment

    public virtual ActionResult LaadVergelijkingOpMoment2Items(string grafiektitel, string item1, string item2, string gewensteData, string soortGrafiek)
    {

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<dynamic> xLabels = new List<dynamic>();
      List<double> data = new List<double>();

      GrafiekWaarde grafiekwaarde = new GrafiekWaarde();

      List<GrafiekItem> grafiekitems = new List<GrafiekItem>();

      switch (gewensteData)
      {
        case "av":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }

          }

          grafiekwaarde = GrafiekWaarde.Vermeldingen;

          break;
        case "gp":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }

          }

          grafiekwaarde = GrafiekWaarde.Polariteit;


          break;
        case "go":

          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }

          }

          grafiekwaarde = GrafiekWaarde.Objectiviteit;

          break;

      }

      string grafiektype = "";
      bool toonLegende = true;
      bool toonXAs = false;
      bool toonYAs = false;
      if (soortGrafiek.Equals("staaf"))
      {
        grafiektype = "bar";
        toonLegende = false;
        toonXAs = true;
        toonYAs = true;
      }
      else if (soortGrafiek.Equals("taart"))
      {
        grafiektype = "pie";
        toonLegende = true;
        toonXAs = false;
        toonYAs = false;

      }

      ViewBag.Grafiektype = grafiektype;
      ViewBag.ToonLegende = toonLegende;
      ViewBag.ToonXAs = toonXAs;
      ViewBag.ToonYAs = toonYAs;



      ViewBag.XLabels = xLabels;
      ViewBag.Data = data;
      ViewBag.Grafiektitel = grafiektitel;


      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        DashboardId = HuidigDashboard.DashboardId,
        Titel = grafiektitel,
        ToonLegende = toonLegende,
        ToonXAs = toonXAs,
        ToonYAs = toonYAs,

        Type = grafiektype,

        XOorsprongNul = true,
        XTitel = "items",
        YOorsprongNul = true,
        YTitel = "data",
        XLabels = xLabels,

        Datawaarden = new List<List<double>>() { data },

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850" }, null, null, null, null },
        LegendeLijst = new List<dynamic> { null, null, null, null, null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekwaarde,
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;


      return PartialView("~/Views/Shared/Grafieken/StaafdiagramTaartdiagram/StaafdiagramTaartDiagram1Dataset.cshtml", ViewBag);

    }

    public virtual ActionResult LaadVergelijkingOpMoment3Items(string grafiektitel, string item1, string item2, string item3, string gewensteData, string soortGrafiek)
    {


      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<dynamic> xLabels = new List<dynamic>();
      List<double> data = new List<double>();

      GrafiekWaarde grafiekwaarde = new GrafiekWaarde();

      List<GrafiekItem> grafiekitems = new List<GrafiekItem>();

      switch (gewensteData)
      {
        case "av":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }


          }

          grafiekwaarde = GrafiekWaarde.Vermeldingen;

          break;
        case "gp":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }

          }

          grafiekwaarde = GrafiekWaarde.Polariteit;


          break;
        case "go":

          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }


          }

          grafiekwaarde = GrafiekWaarde.Objectiviteit;

          break;

      }

      string grafiektype = "";
      bool toonLegende = true;
      bool toonXAs = false;
      bool toonYAs = false;
      if (soortGrafiek.Equals("staaf"))
      {
        grafiektype = "bar";
        toonLegende = false;
        toonXAs = true;
        toonYAs = true;
      }
      else if (soortGrafiek.Equals("taart"))
      {
        grafiektype = "pie";
        toonLegende = true;
        toonXAs = false;
        toonYAs = false;

      }

      ViewBag.Grafiektype = grafiektype;
      ViewBag.ToonLegende = toonLegende;
      ViewBag.ToonXAs = toonXAs;
      ViewBag.ToonYAs = toonYAs;



      ViewBag.XLabels = xLabels;
      ViewBag.Data = data;
      ViewBag.Grafiektitel = grafiektitel;


      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,

        DashboardId = HuidigDashboard.DashboardId,

        Titel = grafiektitel,
        ToonLegende = toonLegende,
        ToonXAs = toonXAs,
        ToonYAs = toonYAs,

        Type = grafiektype,

        XOorsprongNul = true,
        XTitel = "items",
        YOorsprongNul = true,
        YTitel = "data",
        XLabels = xLabels,

        Datawaarden = new List<List<double>>() { data },

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850" }, null, null, null, null },
        LegendeLijst = new List<dynamic> { null, null, null, null, null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekwaarde,
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;


      return PartialView("~/Views/Shared/Grafieken/StaafdiagramTaartdiagram/StaafdiagramTaartDiagram1Dataset.cshtml", ViewBag);

    }


    public virtual ActionResult LaadVergelijkingOpMoment4Items(string grafiektitel, string item1, string item2, string item3, string item4, string item5, string gewensteData, string soortGrafiek)
    {


      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<dynamic> xLabels = new List<dynamic>();
      List<double> data = new List<double>();

      GrafiekWaarde grafiekwaarde = new GrafiekWaarde();

      List<GrafiekItem> grafiekitems = new List<GrafiekItem>();

      switch (gewensteData)
      {
        case "av":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }

          }

          grafiekwaarde = GrafiekWaarde.Vermeldingen;

          break;
        case "gp":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }


          }

          grafiekwaarde = GrafiekWaarde.Polariteit;


          break;
        case "go":

          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }

          }

          grafiekwaarde = GrafiekWaarde.Objectiviteit;

          break;

      }

      string grafiektype = "";
      bool toonLegende = true;
      bool toonXAs = false;
      bool toonYAs = false;
      if (soortGrafiek.Equals("staaf"))
      {
        grafiektype = "bar";
        toonLegende = false;
        toonXAs = true;
        toonYAs = true;
      }
      else if (soortGrafiek.Equals("taart"))
      {
        grafiektype = "pie";
        toonLegende = true;
        toonXAs = false;
        toonYAs = false;

      }

      ViewBag.Grafiektype = grafiektype;
      ViewBag.ToonLegende = toonLegende;
      ViewBag.ToonXAs = toonXAs;
      ViewBag.ToonYAs = toonYAs;



      ViewBag.XLabels = xLabels;
      ViewBag.Data = data;
      ViewBag.Grafiektitel = grafiektitel;


      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        //Nog aanpassen
        DashboardId = HuidigDashboard.DashboardId,

        Titel = grafiektitel,
        ToonLegende = toonLegende,
        ToonXAs = toonXAs,
        ToonYAs = toonYAs,

        Type = grafiektype,

        XOorsprongNul = true,
        XTitel = "items",
        YOorsprongNul = true,
        YTitel = "data",
        XLabels = xLabels,

        Datawaarden = new List<List<double>>() { data },

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850" }, null, null, null, null },
        LegendeLijst = new List<dynamic> { null, null, null, null, null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekwaarde,
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;


      return PartialView("~/Views/Shared/Grafieken/StaafdiagramTaartdiagram/StaafdiagramTaartDiagram1Dataset.cshtml", ViewBag);

    }

    public virtual ActionResult LaadVergelijkingOpMoment5Items(string grafiektitel, string item1, string item2, string item3, string item4, string item5, string gewensteData, string soortGrafiek)
    {

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<dynamic> xLabels = new List<dynamic>();
      List<double> data = new List<double>();

      GrafiekWaarde grafiekwaarde = new GrafiekWaarde();

      List<GrafiekItem> grafiekitems = new List<GrafiekItem>();

      switch (gewensteData)
      {
        case "av":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item5))
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
          }

          grafiekwaarde = GrafiekWaarde.Vermeldingen;

          break;
        case "gp":
          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item5))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }

          }

          grafiekwaarde = GrafiekWaarde.Polariteit;


          break;
        case "go":

          foreach (var element in items)
          {
            if (element.Naam.Equals(item1))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item2))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item3))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item4))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.Naam.Equals(item5))
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
          }

          grafiekwaarde = GrafiekWaarde.Objectiviteit;

          break;

      }

      string grafiektype = "";
      bool toonLegende = true;
      bool toonXAs = false;
      bool toonYAs = false;
      if (soortGrafiek.Equals("staaf"))
      {
        grafiektype = "bar";
        toonLegende = false;
        toonXAs = true;
        toonYAs = true;
      }
      else if (soortGrafiek.Equals("taart"))
      {
        grafiektype = "pie";
        toonLegende = true;
        toonXAs = false;
        toonYAs = false;

      }

      ViewBag.Grafiektype = grafiektype;
      ViewBag.ToonLegende = toonLegende;
      ViewBag.ToonXAs = toonXAs;
      ViewBag.ToonYAs = toonYAs;



      ViewBag.XLabels = xLabels;
      ViewBag.Data = data;
      ViewBag.Grafiektitel = grafiektitel;


      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        //Nog aanpassen
        DashboardId = HuidigDashboard.DashboardId,

        Titel = grafiektitel,
        ToonLegende = toonLegende,
        ToonXAs = toonXAs,
        ToonYAs = toonYAs,

        Type = grafiektype,

        XOorsprongNul = true,
        XTitel = "items",
        YOorsprongNul = true,
        YTitel = "data",
        XLabels = xLabels,

        Datawaarden = new List<List<double>>() { data },

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850" }, null, null, null, null },
        LegendeLijst = new List<dynamic> { null, null, null, null, null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekwaarde,
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;


      return PartialView("~/Views/Shared/Grafieken/StaafdiagramTaartdiagram/StaafdiagramTaartDiagram1Dataset.cshtml", ViewBag);

    }
    #endregion




    #region aantal tweets
    public virtual ActionResult LaadLijndiagramAantalTweets(string grafiektitel, string item, string aantalDagen, string mannenVrouwen)
    {

      int dagen = Int32.Parse(aantalDagen);

      List<ItemHistoriek> grafiekItemhistorieken = new List<ItemHistoriek>();


      List<dynamic> grafiekXLabels = new List<dynamic>();
      List<double> grafiekWaarden = new List<double>();
      List<double> grafiekWaarden2 = new List<double>();
      List<dynamic> legendelijst = new List<dynamic>();
      bool legendeTonen = false;

      List<GrafiekItem> grafiekItems = new List<GrafiekItem>();
      List<GemonitordItem> gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      GemonitordItem gemonitordItem = new GemonitordItem();
      List<List<double>> waarden = new List<List<double>>();
      string link = "";
      GrafiekWaarde grafiekWaarde = new GrafiekWaarde();

      foreach (var element in gemonitordeItems)
      {
        if (element.Naam.Equals(item))
        {
          grafiekItems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
          gemonitordItem = element;
        }
      }


      grafiekItemhistorieken = gemonitordItem.ItemHistorieken;

      for (int i = grafiekItemhistorieken.Count - dagen; i < grafiekItemhistorieken.Count; i++)
      {
        grafiekXLabels.Add(grafiekItemhistorieken[i].HistoriekDatum.ToShortDateString());

        switch (mannenVrouwen)
        {
          case "a":
            grafiekWaarden.Add(grafiekItemhistorieken[i].AantalVermeldingen);
            link = "~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram1Item.cshtml";
            waarden.Add(grafiekWaarden);
           
            legendeTonen = false;
            grafiekWaarde = GrafiekWaarde.Vermeldingen;
            break;
          case "v":
            grafiekWaarden.Add(grafiekItemhistorieken[i].AantalBerichtenVanVrouwen);
            link = "~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram1Item.cshtml";
            waarden.Add(grafiekWaarden);
     
            legendeTonen = false;
            grafiekWaarde = GrafiekWaarde.VermeldingenVrouwen;
            break;
          case "m":
            grafiekWaarden.Add(grafiekItemhistorieken[i].AantalBerichtenVanMannen);
            link = "~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram1Item.cshtml";
            waarden.Add(grafiekWaarden);

            legendeTonen = false;
            grafiekWaarde = GrafiekWaarde.VermeldingenMannen;
            break;
          case "mv":
            grafiekWaarden.Add(grafiekItemhistorieken[i].AantalBerichtenVanMannen);
            grafiekWaarden2.Add(grafiekItemhistorieken[i].AantalBerichtenVanVrouwen);
            link = "~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram2Items.cshtml";
            waarden.Add(grafiekWaarden);
            waarden.Add(grafiekWaarden2);

            legendelijst.Add("mannen");
            legendelijst.Add("vrouwen");
     
            legendeTonen = true;
            grafiekWaarde = GrafiekWaarde.VermeldingenMannenVrouwen;
            break;
        }

      }

      ViewBag.Data = waarden;

      ViewBag.XLabels = grafiekXLabels;
      ViewBag.Grafiektitel = grafiektitel;
      ViewBag.Legendelijst = legendelijst;



      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        //Nog aanpassen
        DashboardId = HuidigDashboard.DashboardId,

        Titel = grafiektitel,
        ToonLegende = legendeTonen,
        ToonXAs = true,
        ToonYAs = true,

        Type = "line",

        XOorsprongNul = true,
        XTitel = "Datum",
        YOorsprongNul = true,
        YTitel = "Aantal tweets",
        XLabels = grafiekXLabels,
        Periode = dagen,

        Datawaarden = new List<List<double>>() { waarden[0], waarden[1] },

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, null, null, null },
        LegendeLijst = legendelijst,

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = true,

        GrafiekItems = grafiekItems,
        GrafiekWaarde = grafiekWaarde
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;

      return PartialView(link, ViewBag);
    }

    #endregion



    #region vergelijking doorheen tijd
    public virtual ActionResult LaadVergelijkingDoorheenTijd1Item(string grafiektitel, string item1, string aantalDagen, string gewensteData)
    {

      int dagen = Int32.Parse(aantalDagen);

      List<ItemHistoriek> grafiekItemhistorieken = new List<ItemHistoriek>();


      List<dynamic> grafiekXLabels = new List<dynamic>();
      List<double> grafiekWaarden = new List<double>();
      List<GrafiekItem> grafiekItems = new List<GrafiekItem>();
      List<GemonitordItem> gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      GemonitordItem gemonitordItem = new GemonitordItem();

      foreach (var element in gemonitordeItems)
      {
        if (element.Naam.Equals(item1))
        {
          grafiekItems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
          gemonitordItem = element;
        }
      }


      grafiekItemhistorieken = gemonitordItem.ItemHistorieken;

      for (int i = grafiekItemhistorieken.Count - dagen; i < grafiekItemhistorieken.Count; i++)
      {
        grafiekXLabels.Add(grafiekItemhistorieken[i].HistoriekDatum.ToShortDateString());

        switch (gewensteData)
        {
          case "av":
            grafiekWaarden.Add(grafiekItemhistorieken[i].AantalVermeldingen);

            break;

          case "gp":
            grafiekWaarden.Add(grafiekItemhistorieken[i].GemPolariteit);

            break;

          case "go":
            grafiekWaarden.Add(grafiekItemhistorieken[i].GemObjectiviteit);

            break;
        }

      }

      ViewBag.ItemDagen = grafiekXLabels;
      ViewBag.Item1Data = grafiekWaarden;
      ViewBag.Grafiektitel = grafiektitel;




      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        //Nog aanpassen
        DashboardId = HuidigDashboard.DashboardId,

        Titel = grafiektitel,
        ToonLegende = false,
        ToonXAs = true,
        ToonYAs = true,

        Type = "line",

        XOorsprongNul = true,
        XTitel = "Datum",
        YOorsprongNul = true,
        YTitel = "Aantal tweets",
        XLabels = grafiekXLabels,
        Periode = dagen,

        Datawaarden = new List<List<double>>() { grafiekWaarden },

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, null, null, null, null },
        LegendeLijst = new List<dynamic>() { null, null, null, null, null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiekItems,
        GrafiekWaarde = GrafiekWaarde.Vermeldingen,
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;

      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram1Item.cshtml", ViewBag);

    }


    public virtual ActionResult LaadVergelijkingDoorheenTijd2Items(string grafiektitel, string item1, string item2, string aantalDagen, string gewensteData)
    {

      int dagen = Int32.Parse(aantalDagen);
      GrafiekWaarde grafiekWaarde = new GrafiekWaarde();

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<dynamic> grafiekXLabels = new List<dynamic>();
      List<ItemHistoriek> grafiekItemhistorieken = new List<ItemHistoriek>();
      List<double> grafiekWaarden = new List<double>();
      List<string> grafiekLegendelijst = new List<string>();

      List<ItemHistoriek> itemhistoriekItem1 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem2 = new List<ItemHistoriek>();

      List<double> waardenItem1 = new List<double>();
      List<double> waardenItem2 = new List<double>();

      List<List<double>> alleWaarden = new List<List<double>>();
      List<GrafiekItem> grafiekitems = new List<GrafiekItem>();


      //List<DateTime> dagenItems = new List<DateTime>();


      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });

        }
        if (element.Naam.Equals(item2))
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }

      }


      for (int i = itemhistoriekItem1.Count - dagen; i < itemhistoriekItem1.Count; i++)
      {
        grafiekXLabels.Add(itemhistoriekItem1[i].HistoriekDatum.ToShortDateString());

        switch (gewensteData)
        {
          case "av":
            waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
            waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);

            grafiekWaarde = GrafiekWaarde.Vermeldingen;
            break;
          case "gp":
            waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);

            grafiekWaarde = GrafiekWaarde.Polariteit;

            break;
          case "go":
            waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);

            grafiekWaarde = GrafiekWaarde.Objectiviteit;

            break;
        }

      }

      alleWaarden.Add(waardenItem1);
      alleWaarden.Add(waardenItem2);

      ViewBag.Data = alleWaarden;
      ViewBag.Legendelijst = grafiekLegendelijst;
      ViewBag.Grafiektitel = grafiektitel;
      ViewBag.XLabels = grafiekXLabels;



      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        //Nog aanpassen
        DashboardId = HuidigDashboard.DashboardId,

        Titel = grafiektitel,
        ToonLegende = true,
        ToonXAs = true,
        ToonYAs = true,

        Type = "line",

        XOorsprongNul = true,
        XTitel = "Datum",
        YOorsprongNul = true,
        YTitel = "Data",
        XLabels = grafiekXLabels,
        Periode = dagen,

        Datawaarden = new List<List<double>>() { alleWaarden[0], alleWaarden[1] },

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, null, null, null },
        LegendeLijst = new List<dynamic> { grafiekLegendelijst[0], grafiekLegendelijst[1], null, null, null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = true,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekWaarde
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;




      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram2Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadVergelijkingDoorheenTijd3Items(string grafiektitel, string item1, string item2, string item3, string aantalDagen, string gewensteData)
    {

      int dagen = Int32.Parse(aantalDagen);
      GrafiekWaarde grafiekWaarde = new GrafiekWaarde();

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<dynamic> grafiekXLabels = new List<dynamic>();
      List<ItemHistoriek> grafiekItemhistorieken = new List<ItemHistoriek>();
      List<double> grafiekWaarden = new List<double>();
      List<string> grafiekLegendelijst = new List<string>();

      List<ItemHistoriek> itemhistoriekItem1 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem2 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem3 = new List<ItemHistoriek>();


      List<double> waardenItem1 = new List<double>();
      List<double> waardenItem2 = new List<double>();
      List<double> waardenItem3 = new List<double>();


      List<List<double>> alleWaarden = new List<List<double>>();
      List<GrafiekItem> grafiekitems = new List<GrafiekItem>();



      //List<DateTime> dagenItems = new List<DateTime>();


      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });

        }
        if (element.Naam.Equals(item2))
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.Naam.Equals(item3))
        {
          itemhistoriekItem3 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }


      }


      for (int i = itemhistoriekItem1.Count - dagen; i < itemhistoriekItem1.Count; i++)
      {
        grafiekXLabels.Add(itemhistoriekItem1[i].HistoriekDatum.ToShortDateString());

        switch (gewensteData)
        {
          case "av":
            waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
            waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
            waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);

            grafiekWaarde = GrafiekWaarde.Vermeldingen;
            break;
          case "gp":
            waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);

            grafiekWaarde = GrafiekWaarde.Polariteit;

            break;
          case "go":
            waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);

            grafiekWaarde = GrafiekWaarde.Objectiviteit;

            break;
        }

      }

      alleWaarden.Add(waardenItem1);
      alleWaarden.Add(waardenItem2);
      alleWaarden.Add(waardenItem3);



      ViewBag.Data = alleWaarden;
      ViewBag.Legendelijst = grafiekLegendelijst;
      ViewBag.Grafiektitel = grafiektitel;
      ViewBag.XLabels = grafiekXLabels;



      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        //Nog aanpassen
        DashboardId = HuidigDashboard.DashboardId,

        Titel = grafiektitel,
        ToonLegende = true,
        ToonXAs = true,
        ToonYAs = true,

        Type = "line",

        XOorsprongNul = true,
        XTitel = "Datum",
        YOorsprongNul = true,
        YTitel = "Data",
        XLabels = grafiekXLabels,
        Periode = dagen,

        Datawaarden = new List<List<double>>() { alleWaarden[0], alleWaarden[1], alleWaarden[2] },

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, new List<string> { "#3cba9f" }, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, new List<string> { "#3cba9f" }, null, null },
        LegendeLijst = new List<dynamic> { grafiekLegendelijst[0], grafiekLegendelijst[1], grafiekLegendelijst[2], null, null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = true,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekWaarde
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;


      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram3Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadVergelijkingDoorheenTijd4Items(string grafiektitel, string item1, string item2, string item3, string item4, string aantalDagen, string gewensteData)
    {

      int dagen = Int32.Parse(aantalDagen);
      GrafiekWaarde grafiekWaarde = new GrafiekWaarde();

      items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      List<dynamic> grafiekXLabels = new List<dynamic>();
      List<ItemHistoriek> grafiekItemhistorieken = new List<ItemHistoriek>();
      List<double> grafiekWaarden = new List<double>();
      List<string> grafiekLegendelijst = new List<string>();

      List<ItemHistoriek> itemhistoriekItem1 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem2 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem3 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem4 = new List<ItemHistoriek>();

      List<double> waardenItem1 = new List<double>();
      List<double> waardenItem2 = new List<double>();
      List<double> waardenItem3 = new List<double>();
      List<double> waardenItem4 = new List<double>();

      List<List<double>> alleWaarden = new List<List<double>>();
      List<GrafiekItem> grafiekitems = new List<GrafiekItem>();



      //List<DateTime> dagenItems = new List<DateTime>();


      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });

        }
        if (element.Naam.Equals(item2))
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.Naam.Equals(item3))
        {
          itemhistoriekItem3 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.Naam.Equals(item4))
        {
          itemhistoriekItem4 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }


      }


      for (int i = itemhistoriekItem1.Count - dagen; i < itemhistoriekItem1.Count; i++)
      {
        grafiekXLabels.Add(itemhistoriekItem1[i].HistoriekDatum.ToShortDateString());

        switch (gewensteData)
        {
          case "av":
            waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
            waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
            waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);
            waardenItem4.Add(itemhistoriekItem4[i].AantalVermeldingen);
            grafiekWaarde = GrafiekWaarde.Vermeldingen;
            break;
          case "gp":
            waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);
            waardenItem4.Add(itemhistoriekItem4[i].GemPolariteit);
            grafiekWaarde = GrafiekWaarde.Polariteit;

            break;
          case "go":
            waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);
            waardenItem4.Add(itemhistoriekItem4[i].GemObjectiviteit);
            grafiekWaarde = GrafiekWaarde.Objectiviteit;

            break;
        }

      }

      alleWaarden.Add(waardenItem1);
      alleWaarden.Add(waardenItem2);
      alleWaarden.Add(waardenItem3);
      alleWaarden.Add(waardenItem4);


      ViewBag.Data = alleWaarden;
      ViewBag.Legendelijst = grafiekLegendelijst;
      ViewBag.Grafiektitel = grafiektitel;
      ViewBag.XLabels = grafiekXLabels;



      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        //Nog aanpassen
        DashboardId = HuidigDashboard.DashboardId,

        Titel = grafiektitel,
        ToonLegende = true,
        ToonXAs = true,
        ToonYAs = true,

        Type = "line",

        XOorsprongNul = true,
        XTitel = "Datum",
        YOorsprongNul = true,
        YTitel = "Data",
        XLabels = grafiekXLabels,
        Periode = dagen,

        Datawaarden = new List<List<double>>() { alleWaarden[0], alleWaarden[1], alleWaarden[2], alleWaarden[3] },

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, new List<string> { "#3cba9f" }, new List<string> { "#e8c3b9" }, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, new List<string> { "#3cba9f" }, new List<string> { "#e8c3b9" }, null },
        LegendeLijst = new List<dynamic> { grafiekLegendelijst[0], grafiekLegendelijst[1], grafiekLegendelijst[2], grafiekLegendelijst[3], null },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = true,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekWaarde
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;




      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram4Items.cshtml", ViewBag);
    }




    public virtual ActionResult LaadVergelijkingDoorheenTijd5Items(string grafiektitel, string item1, string item2, string item3, string item4, string item5, string aantalDagen, string gewensteData)
    {

      int dagen = Int32.Parse(aantalDagen);
      GrafiekWaarde grafiekWaarde = new GrafiekWaarde();

      items = itemManager.GetGemonitordeItems(1).ToList();

      List<dynamic> grafiekXLabels = new List<dynamic>();
      List<ItemHistoriek> grafiekItemhistorieken = new List<ItemHistoriek>();
      List<double> grafiekWaarden = new List<double>();
      List<string> grafiekLegendelijst = new List<string>();

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
      List<GrafiekItem> grafiekitems = new List<GrafiekItem>();



      //List<DateTime> dagenItems = new List<DateTime>();


      foreach (var element in items)
      {
        if (element.Naam.Equals(item1))
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });

        }
        if (element.Naam.Equals(item2))
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.Naam.Equals(item3))
        {
          itemhistoriekItem3 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.Naam.Equals(item4))
        {
          itemhistoriekItem4 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.Naam.Equals(item5))
        {
          itemhistoriekItem5 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }

      }


      for (int i = itemhistoriekItem1.Count - dagen; i < itemhistoriekItem1.Count; i++)
      {
        grafiekXLabels.Add(itemhistoriekItem1[i].HistoriekDatum.ToShortDateString());

        switch (gewensteData)
        {
          case "av":
            waardenItem1.Add(itemhistoriekItem1[i].AantalVermeldingen);
            waardenItem2.Add(itemhistoriekItem2[i].AantalVermeldingen);
            waardenItem3.Add(itemhistoriekItem3[i].AantalVermeldingen);
            waardenItem4.Add(itemhistoriekItem4[i].AantalVermeldingen);
            waardenItem5.Add(itemhistoriekItem5[i].AantalVermeldingen);
            grafiekWaarde = GrafiekWaarde.Vermeldingen;
            break;
          case "gp":
            waardenItem1.Add(itemhistoriekItem1[i].GemPolariteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemPolariteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemPolariteit);
            waardenItem4.Add(itemhistoriekItem4[i].GemPolariteit);
            waardenItem5.Add(itemhistoriekItem5[i].GemPolariteit);
            grafiekWaarde = GrafiekWaarde.Polariteit;

            break;
          case "go":
            waardenItem1.Add(itemhistoriekItem1[i].GemObjectiviteit);
            waardenItem2.Add(itemhistoriekItem2[i].GemObjectiviteit);
            waardenItem3.Add(itemhistoriekItem3[i].GemObjectiviteit);
            waardenItem4.Add(itemhistoriekItem4[i].GemObjectiviteit);
            waardenItem5.Add(itemhistoriekItem5[i].GemObjectiviteit);
            grafiekWaarde = GrafiekWaarde.Objectiviteit;

            break;
        }

      }

      alleWaarden.Add(waardenItem1);
      alleWaarden.Add(waardenItem2);
      alleWaarden.Add(waardenItem3);
      alleWaarden.Add(waardenItem4);
      alleWaarden.Add(waardenItem5);


      ViewBag.Data = alleWaarden;
      ViewBag.Legendelijst = grafiekLegendelijst;
      ViewBag.Grafiektitel = grafiektitel;
      ViewBag.XLabels = grafiekXLabels;



      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
        //Nog aanpassen
        DashboardId = HuidigDashboard.DashboardId,

        Titel = grafiektitel,
        ToonLegende = true,
        ToonXAs = true,
        ToonYAs = true,

        Type = "line",

        XOorsprongNul = true,
        XTitel = "Datum",
        YOorsprongNul = true,
        YTitel = "Data",
        XLabels = grafiekXLabels,
        Periode = dagen,

        Datawaarden = new List<List<double>>() { alleWaarden[0], alleWaarden[1], alleWaarden[2], alleWaarden[3], alleWaarden[4] },

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, new List<string> { "#3cba9f" }, new List<string> { "#e8c3b9" }, new List<string> { "#c45850" } },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, new List<string> { "#3cba9f" }, new List<string> { "#e8c3b9" }, new List<string> { "#c45850" } },
        LegendeLijst = new List<dynamic> { grafiekLegendelijst[0], grafiekLegendelijst[1], grafiekLegendelijst[2], grafiekLegendelijst[3], grafiekLegendelijst[4] },

        XAsMaxrotatie = 90,
        XAsMinrotatie = 90,
        FillDataset = false,
        Lijnlegendeweergave = true,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekWaarde
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;




      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram5Items.cshtml", ViewBag);
    }

    #endregion




    #region kruising grafiek

    public virtual ActionResult LaadKruising(string grafiektitel, string item1, string item2, string aantalDagen)
    {

      int dagen = Int32.Parse(aantalDagen);

      List<ItemHistoriek> itemhistorieken = new List<ItemHistoriek>();
      List<dynamic> grafiekXLabels = new List<dynamic>();
      List<double> grafiekWaarden = new List<double>();
      List<GemonitordItem> gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();

      GemonitordItem gemonitordItem1 = new GemonitordItem();
      GemonitordItem gemonitordItem2 = new GemonitordItem(); ;
      GekruistItem gekruistItem = new GekruistItem();



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

      gekruistItem = new GekruistItem()
      {
        Item1 = gemonitordItem1,
        Item2 = gemonitordItem2
      };


      gekruistItem.BerekenEigenschappen();

      itemManager.MaakHistorieken(gekruistItem, HuidigDeelplatform.AantalDagenHistoriek, HuidigDeelplatform.LaatsteSynchronisatie);

      foreach (var item in gekruistItem.ItemHistorieken)
      {
        itemhistorieken.Add(item);
      }


      //for (int i = itemhistorieken.Count - dagen; i < itemhistorieken.Count; i++)
      //{
      //  grafiekXLabels.Add(itemhistorieken[i].HistoriekDatum.ToShortDateString());
      //  grafiekWaarden.Add(itemhistorieken[i].AantalVermeldingen);
      //}

      for (int i = 0; i < itemhistorieken.Count; i++)
      {
        grafiekXLabels.Add(itemhistorieken[i].HistoriekDatum.ToShortDateString());
        grafiekWaarden.Add(itemhistorieken[i].AantalVermeldingen);
      }


      ViewBag.Grafiektitel = grafiektitel;
      ViewBag.ItemDagen = grafiekXLabels;
      ViewBag.ItemAantalTweets = grafiekWaarden;


      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/LijndiagramAantalTweets.cshtml", ViewBag);
    }

    #endregion





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





    //public ActionResult VoegGrafiekToeEnUpdateDashboard()
    //{
    //  GrafiekenManager grafiekenManager = new GrafiekenManager();

    //  List<Grafiek> alleGrafieken = grafiekenManager.GetGrafiekenTest();


    //  foreach (var item in alleGrafieken)
    //  {
    //    grafiekenManager.AddGrafiek(item);
    //  }


    //  //foreach (var grafiek in alleGrafieken)
    //  //{
    //  //  grafiekenManager.AddGrafiek(grafiek);
    //  //}

    //  return RedirectToAction("Index");
    //}




    //public ActionResult UpdateGrafiekEnUpdateDashboard(int grafiekId, int deelplatformId, string titel,
    //  int periode, bool toonLegende, bool toonXAs, bool toonYAs, int keuze, string xTitel, string yTitel,
    //  bool xOnder, bool xOorsprongNul, bool yOorsprongNul, int dashboardId,
    //  string item1 = null, string waarde1 = "Vermeldingen",
    //  string item2 = null, string waarde2 = "Vermeldingen",
    //  string item3 = null, string waarde3 = "Vermeldingen",
    //  string item4 = null, string waarde4 = "Vermeldingen",
    //  string item5 = null, string waarde5 = "Vermeldingen")
    //{
    //  GrafiekenManager grafiekenManager = new GrafiekenManager();
    //  GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
    //  List<GrafiekItem> grafiekItems = new List<GrafiekItem>();
    //  List<GemonitordItem> items = itemManager.GetGemonitordeItems(deelplatformId).ToList();
    //  List<GrafiekWaarde> waarden = new List<GrafiekWaarde>();

    //  List<string> itemStrings = new List<string>() { item1, item2, item3, item4, item5 };
    //  List<GrafiekWaarde> tijdelijkeWaarden = new List<GrafiekWaarde>() {
    //    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
    //    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
    //    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
    //    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
    //    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true)
    //  };

    //  GrafiekKeuze grafiekKeuze = GrafiekKeuze.EvolutieAantalVermeldingen1Item;
    //  switch (keuze)
    //  {
    //    case 1: grafiekKeuze = GrafiekKeuze.KruisingTaart; break;
    //    case 2: grafiekKeuze = GrafiekKeuze.KruisingBar; break;
    //    case 3: grafiekKeuze = GrafiekKeuze.EvolutieAantalVermeldingen1Item; break;
    //    case 4: grafiekKeuze = GrafiekKeuze.VergelijkingItemsDoorheenDeTijd; break;
    //    case 5: grafiekKeuze = GrafiekKeuze.VergelijkingItemsOp1Moment; break;
    //  }

    //  int teller = 0;
    //  foreach (string itemString in itemStrings)
    //  {
    //    if (itemString != null)
    //    {
    //      foreach (GemonitordItem item in items)
    //      {
    //        if (item.Naam.Equals(itemString))
    //        {
    //          grafiekItems.Add(new GrafiekItem { ItemId = item.GemonitordItemId });
    //          waarden.Add(tijdelijkeWaarden.ElementAt(teller));
    //        }
    //      }
    //    }
    //    teller++;
    //  }

    //  Grafiek grafiek = new Grafiek()
    //  {
    //    GrafiekId = grafiekId,
    //    Titel = titel,
    //    Periode = periode,
    //    ToonLegende = toonLegende,
    //    ToonXAs = toonXAs,
    //    ToonYAs = toonYAs,
    //    Keuze = grafiekKeuze,
    //    XTitel = xTitel,
    //    YTitel = yTitel,
    //    Waarden = waarden,
    //    XOnder = xOnder,
    //    XOorsprongNul = xOorsprongNul,
    //    YOorsprongNul = yOorsprongNul,
    //    DashboardId = dashboardId,
    //    GrafiekItems = grafiekItems
    //  };

    //  grafiekenManager.ChangeGrafiek(grafiek);
    //  return RedirectToAction("Index");
    //}

    //public ActionResult VerwijderGrafiekEnUpdateDashboard(int grafiekId)
    //{
    //  GrafiekenManager grafiekenManager = new GrafiekenManager();
    //  Grafiek grafiek = new Grafiek()
    //  {
    //    GrafiekId = grafiekId
    //  };

    //  grafiekenManager.RemoveGrafiek(grafiek);
    //  return RedirectToAction("Index");
    //}

    //public void GetData()
    //{
    //  GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
    //  DeelplatformenManager deelplatformenManager = new DeelplatformenManager();

    //  deelplatformenManager.AddDeelplatform(new Deelplatform() { Naam = "Politieke Barometer", AantalDagenHistoriek = 2, LaatsteSynchronisatie = DateTime.Now.AddYears(-100) });
    //  int id = deelplatformenManager.GetDeelplatformByName("Politieke Barometer").DeelplatformId;
    //  gemonitordeItemsManager.AddOrganisatie("Open VLD", id, new List<string>() { "Alexander De Croo", "Gwendolyn Rutten", "Maggie De Block" });
    //  gemonitordeItemsManager.AddOrganisatie("Groen", id, new List<string>() { "Kristof Calvo", "Meyrem Almaci", "Wouter Van Besien" });
    //  gemonitordeItemsManager.AddOrganisatie("SPA", id, new List<string>() { "Caroline Gennez", "John Crombez", "Bruno Tobback" });
    //  gemonitordeItemsManager.AddOrganisatie("Vlaams Belang", id, new List<string>() { "Filip Dewinter", "Tom Van Grieken", "Gerolf Annemans" });

    //  gemonitordeItemsManager.AddThema("Migratie", new List<string>() { "buitenland", "vluchteling", "immigratie", "migratie" }, id);
    //  gemonitordeItemsManager.AddThema("Fiscaliteit", new List<string>() { "belastingen", "tax", "btw", "sociale zekerheid" }, id);
    //  gemonitordeItemsManager.AddThema("Milieu", new List<string>() { "kernenergie", "zonnenergie", "steenkool", "luchtvervuiling", "windenergie" }, id);
    //  TextgainController textgainController = new TextgainController();
    //  textgainController.HaalBerichtenOp(deelplatformenManager.GetDeelplatform(id));
    //}






  }
}


//public ActionResult VoegGrafiekToeEnUpdateDashboard(int deelplatformId, int dashboardId, int grafiekId, string titel, string grafiektype,
//    bool toonLegende, bool xOorsprongNul, bool yOorsprongNul, bool toonXAs, bool toonYAs, bool datasetFill, bool lijnlegendeweergave,
//    int xAsMaxRotatie, int xAsMinRotatie, string xTitel, string yTitel, List<dynamic> xLabels, List<string> legendelijst,
//    List<List<double>> datawaarden, List<List<string>> achtergrondkleur, List<List<string>> randkleur)


//public ActionResult VoegGrafiekToeEnUpdateDashboard(int deelplatformId, string titel, int periode, bool toonLegende, bool toonXAs, bool toonYAs, int keuze,
//  string xTitel, string yTitel, bool xOnder, bool xOorsprongNul, bool yOorsprongNul, int dashboardId,
//  string item1 = null, string waarde1 = "Vermeldingen",
//  string item2 = null, string waarde2 = "Vermeldingen",
//  string item3 = null, string waarde3 = "Vermeldingen",
//  string item4 = null, string waarde4 = "Vermeldingen",
//  string item5 = null, string waarde5 = "Vermeldingen")
//{
//  GrafiekenManager grafiekenManager = new GrafiekenManager();
//  GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
//  List<GrafiekItem> grafiekItems = new List<GrafiekItem>();
//  List<GemonitordItem> items = itemManager.GetGemonitordeItems(deelplatformId).ToList();
//  List<GrafiekWaarde> waarden = new List<GrafiekWaarde>();

//  List<string> itemStrings = new List<string>() { item1, item2, item3, item4, item5 };
//  List<GrafiekWaarde> tijdelijkeWaarden = new List<GrafiekWaarde>() {
//    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
//    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
//    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
//    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
//    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true)
//  };

//  GrafiekKeuze grafiekKeuze = GrafiekKeuze.VergelijkingItemsOp1Moment;
//  switch (keuze)
//  {
//    case 1: grafiekKeuze = GrafiekKeuze.KruisingTaart; break;
//    case 2: grafiekKeuze = GrafiekKeuze.KruisingBar; break;
//    case 3: grafiekKeuze = GrafiekKeuze.EvolutieAantalVermeldingen1Item; break;
//    case 4: grafiekKeuze = GrafiekKeuze.VergelijkingItemsDoorheenDeTijd; break;
//    case 5: grafiekKeuze = GrafiekKeuze.VergelijkingItemsOp1Moment; break;
//  }

//  int teller = 0;
//  foreach (string itemString in itemStrings)
//  {
//    if (itemString != null)
//    {
//      foreach (GemonitordItem item in items)
//      {
//        if (item.Naam.Equals(itemString))
//        {
//          grafiekItems.Add(new GrafiekItem { ItemId = item.GemonitordItemId });
//          waarden.Add(tijdelijkeWaarden.ElementAt(teller));
//        }
//      }
//    }
//    teller++;
//  }

//  Grafiek grafiek = new Grafiek()
//  {
//    Titel = titel,
//    Periode = periode,
//    ToonLegende = toonLegende,
//    ToonXAs = toonXAs,
//    ToonYAs = toonYAs,
//    Keuze = grafiekKeuze,
//    XTitel = xTitel,
//    YTitel = yTitel,
//    Waarden = waarden,
//    XOnder = xOnder,
//    XOorsprongNul = xOorsprongNul,
//    YOorsprongNul = yOorsprongNul,
//    DashboardId = dashboardId,
//    GrafiekItems = grafiekItems
//  };

//  grafiekenManager.AddGrafiek(grafiek);
//  return RedirectToAction("Index");
//}

//public ActionResult UpdateGrafiekEnUpdateDashboard(int grafiekId, int deelplatformId, string titel,
//  int periode, bool toonLegende, bool toonXAs, bool toonYAs, int keuze, string xTitel, string yTitel,
//  bool xOnder, bool xOorsprongNul, bool yOorsprongNul, int dashboardId,
//  string item1 = null, string waarde1 = "Vermeldingen",
//  string item2 = null, string waarde2 = "Vermeldingen",
//  string item3 = null, string waarde3 = "Vermeldingen",
//  string item4 = null, string waarde4 = "Vermeldingen",
//  string item5 = null, string waarde5 = "Vermeldingen")
//{
//  GrafiekenManager grafiekenManager = new GrafiekenManager();
//  GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
//  List<GrafiekItem> grafiekItems = new List<GrafiekItem>();
//  List<GemonitordItem> items = itemManager.GetGemonitordeItems(deelplatformId).ToList();
//  List<GrafiekWaarde> waarden = new List<GrafiekWaarde>();

//  List<string> itemStrings = new List<string>() { item1, item2, item3, item4, item5 };
//  List<GrafiekWaarde> tijdelijkeWaarden = new List<GrafiekWaarde>() {
//    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
//    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
//    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
//    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true),
//    (GrafiekWaarde) Enum.Parse(typeof(GrafiekWaarde), waarde1, true)
//  };

//  GrafiekKeuze grafiekKeuze = GrafiekKeuze.EvolutieAantalVermeldingen1Item;
//  switch (keuze)
//  {
//    case 1: grafiekKeuze = GrafiekKeuze.KruisingTaart; break;
//    case 2: grafiekKeuze = GrafiekKeuze.KruisingBar; break;
//    case 3: grafiekKeuze = GrafiekKeuze.EvolutieAantalVermeldingen1Item; break;
//    case 4: grafiekKeuze = GrafiekKeuze.VergelijkingItemsDoorheenDeTijd; break;
//    case 5: grafiekKeuze = GrafiekKeuze.VergelijkingItemsOp1Moment; break;
//  }

//  int teller = 0;
//  foreach (string itemString in itemStrings)
//  {
//    if (itemString != null)
//    {
//      foreach (GemonitordItem item in items)
//      {
//        if (item.Naam.Equals(itemString))
//        {
//          grafiekItems.Add(new GrafiekItem { ItemId = item.GemonitordItemId });
//          waarden.Add(tijdelijkeWaarden.ElementAt(teller));
//        }
//      }
//    }
//    teller++;
//  }

//  Grafiek grafiek = new Grafiek()
//  {
//    GrafiekId = grafiekId,
//    Titel = titel,
//    Periode = periode,
//    ToonLegende = toonLegende,
//    ToonXAs = toonXAs,
//    ToonYAs = toonYAs,
//    Keuze = grafiekKeuze,
//    XTitel = xTitel,
//    YTitel = yTitel,
//    Waarden = waarden,
//    XOnder = xOnder,
//    XOorsprongNul = xOorsprongNul,
//    YOorsprongNul = yOorsprongNul,
//    DashboardId = dashboardId,
//    GrafiekItems = grafiekItems
//  };

//  grafiekenManager.ChangeGrafiek(grafiek);
//  return RedirectToAction("Index");
//}

//public ActionResult VerwijderGrafiekEnUpdateDashboard(int grafiekId)
//{
//  GrafiekenManager grafiekenManager = new GrafiekenManager();
//  Grafiek grafiek = new Grafiek()
//  {
//    GrafiekId = grafiekId
//  };

//  grafiekenManager.RemoveGrafiek(grafiek);
//  return RedirectToAction("Index");
//}

//public void GetData()
//{
//  GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
//  DeelplatformenManager deelplatformenManager = new DeelplatformenManager();

//  deelplatformenManager.AddDeelplatform(new Deelplatform() { Naam = "Politieke Barometer", AantalDagenHistoriek = 2, LaatsteSynchronisatie = DateTime.Now.AddYears(-100) });
//  int id = deelplatformenManager.GetDeelplatformByName("Politieke Barometer").DeelplatformId;
//  gemonitordeItemsManager.AddOrganisatie("Open VLD", id, new List<string>() { "Alexander De Croo", "Gwendolyn Rutten", "Maggie De Block" });
//  gemonitordeItemsManager.AddOrganisatie("Groen", id, new List<string>() { "Kristof Calvo", "Meyrem Almaci", "Wouter Van Besien" });
//  gemonitordeItemsManager.AddOrganisatie("SPA", id, new List<string>() { "Caroline Gennez", "John Crombez", "Bruno Tobback" });
//  gemonitordeItemsManager.AddOrganisatie("Vlaams Belang", id, new List<string>() { "Filip Dewinter", "Tom Van Grieken", "Gerolf Annemans" });

//  gemonitordeItemsManager.AddThema("Migratie", new List<string>() { "buitenland", "vluchteling", "immigratie", "migratie" }, id);
//  gemonitordeItemsManager.AddThema("Fiscaliteit", new List<string>() { "belastingen", "tax", "btw", "sociale zekerheid" }, id);
//  gemonitordeItemsManager.AddThema("Milieu", new List<string>() { "kernenergie", "zonnenergie", "steenkool", "luchtvervuiling", "windenergie" }, id);
//  TextgainController textgainController = new TextgainController();
//  textgainController.HaalBerichtenOp(deelplatformenManager.GetDeelplatform(id));
//}