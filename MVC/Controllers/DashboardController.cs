using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BL;
using Domain.Dashboards;
using Domain.Deelplatformen;
using Domain.Gemonitordeitems;
using Microsoft.AspNet.Identity;

//Seppe

namespace MVC.Controllers
{
  public partial class DashboardController : Controller
  {
    GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
    DashboardsManager dashboardsManager = new DashboardsManager();
    List<SelectListItem> selects = new List<SelectListItem>();
    List<GemonitordItem> items = new List<GemonitordItem>();
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


    public virtual ActionResult LaadLegePartialView()
    {
      return PartialView("~/Views/Shared/LegePartialView.cshtml");
    }


    #region pop-up vensters grafiek toevoegen, aanpassen
    public virtual ActionResult LaadGrafiekToevoegen()
    {

      return PartialView("~/Views/Shared/Dashboard/Grafieken/GrafiekenToevoegen.cshtml");

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
    #endregion


    #region grafiek toevoegen, aanpassen, verwijderen

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

    public ActionResult GrafiekAanpassen(int id, string titel, string xTitel, string yTitel)
    {

      List<Grafiek> grafieken = grafiekenManager.GetGrafieken(HuidigDashboard.DashboardId, HuidigDeelplatform.DeelplatformId, true).ToList();

      foreach (var grafiek in grafieken)
      {
        if (grafiek.GrafiekId == id)
        {
          grafiek.Titel = titel;
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
    #endregion




    #region organisaties laden

    public virtual ActionResult LaadOrganisaties1Item()
    {

      var items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).OrderBy(o => o.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties1Item.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties2Items()
    {
      var items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).OrderBy(o => o.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties2Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties3Items()
    {
      var items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).OrderBy(o => o.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties3Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties4Items()
    {
      var items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).OrderBy(o => o.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties4Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadOrganisaties5Items()
    {
      var items = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).OrderBy(o => o.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.OrganisatiesViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Organisaties/Organisaties5Items.cshtml", ViewBag);
    }

    #endregion


    #region personen laden

    public virtual ActionResult LaadPersonen1Item()
    {
      var items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).OrderBy(p => p.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.PersonenViewbag = selects;


      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen1Item.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen2Items()
    {
      var items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).OrderBy(p => p.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.PersonenViewbag = selects;


      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen2Items.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen3Items()
    {
      var items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).OrderBy(p => p.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.PersonenViewbag = selects;


      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen3Items.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen4Items()
    {
      var items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).OrderBy(p => p.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.PersonenViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen4Items.cshtml", ViewBag);

    }

    public virtual ActionResult LaadPersonen5Items()
    {
      var items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).OrderBy(p => p.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.PersonenViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Personen/Personen5Items.cshtml", ViewBag);

    }

    #endregion


    #region themas laden
    public virtual ActionResult LaadThemas1Item()
    {
      var items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).OrderBy(t => t.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.ThemasViewbag = selects;


      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas1Item.cshtml", ViewBag);
    }

    public virtual ActionResult LaadThemas2Items()
    {
      var items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).OrderBy(t => t.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.ThemasViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas2Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadThemas3Items()
    {
      var items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).OrderBy(t => t.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.ThemasViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas3Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadThemas4Items()
    {
      var items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).OrderBy(t => t.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.ThemasViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas4Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadThemas5Items()
    {
      var items = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).OrderBy(t => t.Naam).ToList();
      selects = new List<SelectListItem>();
      foreach (var item in items)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.ThemasViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/Themas/Themas5Items.cshtml", ViewBag);
    }
    #endregion


    #region laad gemonitorde items
    public virtual ActionResult LaadGemonitordeItems()
    {
      List<GemonitordItem> itemsZonderKruisingen = new List<GemonitordItem>();

      var items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).OrderBy(t => t.Naam).ToList();

      //var items = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId);

      foreach (var item in items)
      {
        if (!(item is GekruistItem))
        {
          itemsZonderKruisingen.Add(item);
        }

      }

      selects = new List<SelectListItem>();
      foreach (var item in itemsZonderKruisingen)
      {
        selects.Add(new SelectListItem() { Text = item.Naam, Value = item.GemonitordItemId.ToString() });
      }

      ViewBag.GemonitordeItemsViewbag = selects;

      return PartialView("~/Views/Shared/Dashboard/Dropdown/GemonitordeItemsAantal.cshtml", ViewBag);

    }
    #endregion


    #region soorten grafieken laden
    public virtual ActionResult LaadAantalTweets()
    {
      ViewBag.AantalDagenHistoriek = HuidigDeelplatform.AantalDagenHistoriek;


      ViewBag.AantalThemas = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).Count();

      return PartialView("~/Views/Shared/Dashboard/Grafieken/AantalTweets.cshtml", ViewBag);
    }

    public virtual ActionResult LaadItemsKruisen()
    {
      ViewBag.AantalDagenHistoriek = HuidigDeelplatform.AantalDagenHistoriek;



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
      ViewBag.AantalDagenHistoriek = HuidigDeelplatform.AantalDagenHistoriek;


      ViewBag.AantalThemas = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).Count();

      return PartialView("~/Views/Shared/Dashboard/Grafieken/VergelijkenDoorheenDeTijd.cshtml");
    }

    public virtual ActionResult LaadVergelijkenOpMoment()
    {
      ViewBag.AantalThemas = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).Count();

      return PartialView("~/Views/Shared/Dashboard/Grafieken/VergelijkenOpMoment.cshtml");
    }
    #endregion



    #region laad vergelijking op moment

    public virtual ActionResult LaadVergelijkingOpMoment2Items(string grafiektitel, string id1, string id2, string gewensteData, string soortGrafiek)
    {
      int id1Int = Int32.Parse(id1);
      int id2Int = Int32.Parse(id2);


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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
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

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850" }, null, null, null, null },
        LegendeLijst = new List<dynamic> { null, null, null, null, null },


        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekwaarde,
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;


      return PartialView("~/Views/Shared/Grafieken/StaafdiagramTaartdiagram/StaafdiagramTaartDiagram1Dataset.cshtml", ViewBag);

    }

    public virtual ActionResult LaadVergelijkingOpMoment3Items(string grafiektitel, string id1, string id2, string id3, string gewensteData, string soortGrafiek)
    {
      int id1Int = Int32.Parse(id1);
      int id2Int = Int32.Parse(id2);
      int id3Int = Int32.Parse(id3);

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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id3Int)
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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id3Int)
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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id3Int)
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

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850" }, null, null, null, null },
        LegendeLijst = new List<dynamic> { null, null, null, null, null },


        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekwaarde,
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;


      return PartialView("~/Views/Shared/Grafieken/StaafdiagramTaartdiagram/StaafdiagramTaartDiagram1Dataset.cshtml", ViewBag);

    }


    public virtual ActionResult LaadVergelijkingOpMoment4Items(string grafiektitel, string id1, string id2, string id3, string id4, string gewensteData, string soortGrafiek)
    {
      int id1Int = Int32.Parse(id1);
      int id2Int = Int32.Parse(id2);
      int id3Int = Int32.Parse(id3);
      int id4Int = Int32.Parse(id4);


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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id3Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id4Int)
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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id3Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id4Int)
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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id3Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id4Int)
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

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850" }, null, null, null, null },
        LegendeLijst = new List<dynamic> { null, null, null, null, null },

        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekwaarde,
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;


      return PartialView("~/Views/Shared/Grafieken/StaafdiagramTaartdiagram/StaafdiagramTaartDiagram1Dataset.cshtml", ViewBag);

    }

    public virtual ActionResult LaadVergelijkingOpMoment5Items(string grafiektitel, string id1, string id2, string id3, string id4, string id5, string gewensteData, string soortGrafiek)
    {
      int id1Int = Int32.Parse(id1);
      int id2Int = Int32.Parse(id2);
      int id3Int = Int32.Parse(id3);
      int id4Int = Int32.Parse(id4);
      int id5Int = Int32.Parse(id5);

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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id3Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id4Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.TotaalAantalVermeldingen);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id5Int)
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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id3Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id4Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemPolariteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id5Int)
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
            if (element.GemonitordItemId == id1Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id2Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id3Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id4Int)
            {
              xLabels.Add(element.Naam);
              data.Add(element.GemObjectiviteit);

              grafiekitems.Add(new GrafiekItem()
              {
                ItemId = element.GemonitordItemId
              });
            }
            if (element.GemonitordItemId == id5Int)
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

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850" }, null, null, null, null },
        LegendeLijst = new List<dynamic> { null, null, null, null, null },


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
    [Authorize]
    public virtual ActionResult LaadLijndiagramAantalTweets(string grafiektitel, string id1, string aantalDagen, string mannenVrouwen)
    {
      int idInt = Int32.Parse(id1);
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
        if (element.GemonitordItemId == idInt)
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

    public virtual ActionResult LaadVergelijkingDoorheenTijd1Item(string grafiektitel, string id1, string aantalDagen, string gewensteData)
    {
      int idInt = Int32.Parse(id1);
      int dagen = Int32.Parse(aantalDagen);

      List<ItemHistoriek> grafiekItemhistorieken = new List<ItemHistoriek>();


      List<dynamic> grafiekXLabels = new List<dynamic>();
      List<double> grafiekWaarden = new List<double>();
      List<List<double>> waarden = new List<List<double>>();
      List<GrafiekItem> grafiekItems = new List<GrafiekItem>();
      List<GemonitordItem> gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      GemonitordItem gemonitordItem = new GemonitordItem();

      foreach (var element in gemonitordeItems)
      {
        if (element.GemonitordItemId == idInt)
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

      waarden.Add(grafiekWaarden);

      ViewBag.XLabels = grafiekXLabels;
      ViewBag.Data = waarden;
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

        Datawaarden = waarden,

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, null, null, null, null },
        LegendeLijst = new List<dynamic>() { null, null, null, null, null },


        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiekItems,
        GrafiekWaarde = GrafiekWaarde.Vermeldingen,
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;

      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram1Item.cshtml", ViewBag);

    }



    public virtual ActionResult LaadVergelijkingDoorheenTijd2Items(string grafiektitel, string id1, string id2, string aantalDagen, string gewensteData)
    {
      int id1Int = Int32.Parse(id1);
      int id2Int = Int32.Parse(id2);

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
        if (element.GemonitordItemId == id1Int)
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });

        }
        if (element.GemonitordItemId == id2Int)
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


        FillDataset = false,
        Lijnlegendeweergave = true,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekWaarde
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;




      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram2Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadVergelijkingDoorheenTijd3Items(string grafiektitel, string id1, string id2, string id3, string aantalDagen, string gewensteData)
    {
      int id1Int = Int32.Parse(id1);
      int id2Int = Int32.Parse(id2);
      int id3Int = Int32.Parse(id3);


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
        if (element.GemonitordItemId == id1Int)
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });

        }
        if (element.GemonitordItemId == id2Int)
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.GemonitordItemId == id3Int)
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


        FillDataset = false,
        Lijnlegendeweergave = true,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekWaarde
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;


      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram3Items.cshtml", ViewBag);
    }

    public virtual ActionResult LaadVergelijkingDoorheenTijd4Items(string grafiektitel, string id1, string id2, string id3, string id4, string aantalDagen, string gewensteData)
    {
      int id1Int = Int32.Parse(id1);
      int id2Int = Int32.Parse(id2);
      int id3Int = Int32.Parse(id3);
      int id4Int = Int32.Parse(id4);

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
        if (element.GemonitordItemId == id1Int)
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });

        }
        if (element.GemonitordItemId == id2Int)
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.GemonitordItemId == id3Int)
        {
          itemhistoriekItem3 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.GemonitordItemId == id4Int)
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

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, new List<string> { "#3cba9f" }, new List<string> { "#ff8000" }, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, new List<string> { "#3cba9f" }, new List<string> { "#ff8000" }, null },
        LegendeLijst = new List<dynamic> { grafiekLegendelijst[0], grafiekLegendelijst[1], grafiekLegendelijst[2], grafiekLegendelijst[3], null },

        FillDataset = false,
        Lijnlegendeweergave = true,

        GrafiekItems = grafiekitems,
        GrafiekWaarde = grafiekWaarde
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;




      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram4Items.cshtml", ViewBag);
    }




    public virtual ActionResult LaadVergelijkingDoorheenTijd5Items(string grafiektitel, string id1, string id2, string id3, string id4, string id5, string aantalDagen, string gewensteData)
    {
      int id1Int = Int32.Parse(id1);
      int id2Int = Int32.Parse(id2);
      int id3Int = Int32.Parse(id3);
      int id4Int = Int32.Parse(id4);
      int id5Int = Int32.Parse(id5);


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
        if (element.GemonitordItemId == id1Int)
        {
          itemhistoriekItem1 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });

        }
        if (element.GemonitordItemId == id2Int)
        {
          itemhistoriekItem2 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.GemonitordItemId == id3Int)
        {
          itemhistoriekItem3 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.GemonitordItemId == id4Int)
        {
          itemhistoriekItem4 = element.ItemHistorieken;
          grafiekLegendelijst.Add(element.Naam);

          grafiekitems.Add(new GrafiekItem()
          {
            ItemId = element.GemonitordItemId
          });
        }
        if (element.GemonitordItemId == id5Int)
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

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, new List<string> { "#3cba9f" }, new List<string> { "#ff8000" }, new List<string> { "#c45850" } },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, new List<string> { "#8e5ea2" }, new List<string> { "#3cba9f" }, new List<string> { "#ff8000" }, new List<string> { "#c45850" } },
        LegendeLijst = new List<dynamic> { grafiekLegendelijst[0], grafiekLegendelijst[1], grafiekLegendelijst[2], grafiekLegendelijst[3], grafiekLegendelijst[4] },


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

    public virtual ActionResult LaadKruising(string grafiektitel, string id1, string id2, string aantalDagen)
    {
      int id1Int = Int32.Parse(id1);
      int id2Int = Int32.Parse(id2);

      int dagen = Int32.Parse(aantalDagen);

      List<ItemHistoriek> itemhistorieken = new List<ItemHistoriek>();
      List<dynamic> grafiekXLabels = new List<dynamic>();
      List<double> grafiekWaarden = new List<double>();
      List<List<double>> waarden = new List<List<double>>();
      List<GemonitordItem> gemonitordeItems = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList();
      List<GrafiekItem> grafiekItems = new List<GrafiekItem>();

      GemonitordItem gemonitordItem1 = new GemonitordItem();
      GemonitordItem gemonitordItem2 = new GemonitordItem(); ;

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
      itemManager.MaakHistorieken(gekruistItem, HuidigDeelplatform.AantalDagenHistoriek, HuidigDeelplatform.LaatsteSynchronisatie);
      itemManager.BerekenEigenschappen(gekruistItem);
      itemManager.ChangeGemonitordItem(gekruistItem);




      grafiekItems.Add(new GrafiekItem()
      {
        ItemId = itemManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList()[index].GemonitordItemId
      });


      itemhistorieken = gekruistItem.ItemHistorieken;


      for (int i = itemhistorieken.Count - dagen; i < itemhistorieken.Count; i++)
      {
        grafiekXLabels.Add(itemhistorieken[i].HistoriekDatum.ToShortDateString());
        grafiekWaarden.Add(itemhistorieken[i].AantalVermeldingen);
      }



      waarden.Add(grafiekWaarden);


      ViewBag.Grafiektitel = grafiektitel;
      ViewBag.XLabels = grafiekXLabels;
      ViewBag.Data = waarden;

      Grafiek grafiek = new Grafiek()
      {
        DeelplatformId = HuidigDeelplatform.DeelplatformId,
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

        Datawaarden = waarden,

        Achtergrondkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, null, null, null, null },
        Randkleur = new List<List<string>>() { new List<string> { "#3e95cd" }, null, null, null, null },
        LegendeLijst = new List<dynamic>() { null, null, null, null, null },

        FillDataset = false,
        Lijnlegendeweergave = true,

        GrafiekItems = grafiekItems,
        GrafiekWaarde = GrafiekWaarde.Vermeldingen
      };

      grafiekenManager.AddGrafiek(grafiek);

      ViewBag.GrafiekId = grafiek.GrafiekId;

      return PartialView("~/Views/Shared/Grafieken/Lijndiagram/Lijndiagram1Item.cshtml", ViewBag);
    }

    #endregion






    #region grafieken niet-ingelogde gebruikers


    public virtual ActionResult LaadGrafiekenNietIngelogd()
    {
      GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();

      var aantalItems = gemonitordeItemsManager.GetGemonitordeItems(HuidigDeelplatform.DeelplatformId).ToList().Count;

      ViewBag.AantalItems = aantalItems;

      if (aantalItems > 0)
      {
        ViewBag.GrafiekenNietIngelogd = GetGrafiekenNietIngelogd();
      }

      return PartialView("~/Views/Shared/Dashboard/Grafieken/GrafiekenNietIngelogd.cshtml", ViewBag);
    }

    public List<Grafiek> GetGrafiekenNietIngelogd()
    {

      GemonitordeItemsManager itemManager = new GemonitordeItemsManager();


      List<GemonitordItem> personen = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).OrderByDescending(p => p.GemPolariteit).ToList();
      List<dynamic> grafiek1XLabels = new List<dynamic>();
      List<double> grafiek1Datawaarden = new List<double>();

      List<GemonitordItem> grafiek1GemonitordeItems = new List<GemonitordItem>();
      List<GrafiekItem> grafiek1Grafiekitems = new List<GrafiekItem>();


      var aantalGrafiek1 = 0;
      if (personen.Count <= 5)
      {
        aantalGrafiek1 = personen.Count;
      }
      else
      {
        aantalGrafiek1 = 5;
      }

      for (int i = 0; i < aantalGrafiek1; i++)
      {
        grafiek1GemonitordeItems.Add(personen[i]);
      }

      foreach (var item in grafiek1GemonitordeItems)
      {
        grafiek1XLabels.Add(item.Naam);
        grafiek1Datawaarden.Add(item.GemPolariteit);
        grafiek1Grafiekitems.Add(new GrafiekItem()
        {
          ItemId = item.GemonitordItemId
        });
      };




      List<GemonitordItem> organisaties = itemManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).OrderByDescending(p => p.GemObjectiviteit).ToList();
      List<dynamic> grafiek2XLabels = new List<dynamic>();
      List<double> grafiek2Datawaarden = new List<double>();

      List<GemonitordItem> grafiek2GemonitordeItems = new List<GemonitordItem>();
      List<GrafiekItem> grafiek2Grafiekitems = new List<GrafiekItem>();

      var aantalGrafiek2 = 0;
      if (organisaties.Count <= 5)
      {
        aantalGrafiek2 = organisaties.Count;
      }
      else
      {
        aantalGrafiek2 = 5;
      }

      for (int i = 0; i < aantalGrafiek2; i++)
      {
        grafiek2GemonitordeItems.Add(organisaties[i]);
      }

      foreach (var item in grafiek2GemonitordeItems)
      {
        grafiek2XLabels.Add(item.Naam);
        grafiek2Datawaarden.Add(item.GemObjectiviteit);
        grafiek2Grafiekitems.Add(new GrafiekItem()
        {
          ItemId = item.GemonitordItemId
        });
      };





      List<GemonitordItem> themas = itemManager.GetThemas(HuidigDeelplatform.DeelplatformId).OrderByDescending(t => t.TotaalAantalVermeldingen).ToList();
      List<GemonitordItem> personenGrafiek3 = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).OrderByDescending(p => p.TotaalAantalVermeldingen).ToList();

      GemonitordItem gemonitordItemGrafiek3 = new GemonitordItem();
      var titelGrafiek3 = "";

      if (themas.Count == 0)
      {
        gemonitordItemGrafiek3 = personenGrafiek3[0];
        titelGrafiek3 = gemonitordItemGrafiek3.Naam;
      }
      else
      {
        gemonitordItemGrafiek3 = themas[0];
        titelGrafiek3 = gemonitordItemGrafiek3.Naam;
      }

      List<dynamic> grafiek3XLabels = new List<dynamic>();
      List<ItemHistoriek> grafiek3Itemhistorieken = new List<ItemHistoriek>();
      List<double> grafiek3Waarden = new List<double>();
      List<GrafiekItem> grafiek3Grafiekitems = new List<GrafiekItem>();

      grafiek3Itemhistorieken = gemonitordItemGrafiek3.ItemHistorieken;
      grafiek3Grafiekitems.Add(new GrafiekItem()
      {
        ItemId = gemonitordItemGrafiek3.GemonitordItemId
      });



      //for (int i = grafiek3Itemhistorieken.Count - 7; i < grafiek3Itemhistorieken.Count; i++)
      //{
      //  grafiek3XLabels.Add(grafiek3Itemhistorieken[i].HistoriekDatum.ToShortDateString());
      //  grafiek3Waarden.Add(grafiek3Itemhistorieken[i].AantalVermeldingen);
      //}

      var aantalGrafiek3 = 0;
      if (grafiek3Itemhistorieken.Count <= 7)
      {
        aantalGrafiek3 = 0;
      }
      else
      {
        aantalGrafiek3 = grafiek3Itemhistorieken.Count - 7;
      }

      for (int i = aantalGrafiek3; i < grafiek3Itemhistorieken.Count; i++)
      {
        grafiek3XLabels.Add(grafiek3Itemhistorieken[i].HistoriekDatum.ToShortDateString());
        grafiek3Waarden.Add(grafiek3Itemhistorieken[i].AantalVermeldingen);
      }







      var grafiek4items = itemManager.GetPersonen(HuidigDeelplatform.DeelplatformId).OrderByDescending(p => p.TotaalAantalVermeldingen).ToList();

      var aantalGrafiek4 = 0;
      List<dynamic> grafiek4XLabels = new List<dynamic>();
      List<ItemHistoriek> grafiek4Itemhistorieken = new List<ItemHistoriek>();
      List<double> grafiek4Waarden = new List<double>();
      List<string> grafiek4Legendelijst = new List<string>();

      List<ItemHistoriek> itemhistoriekItem1Grafiek4 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem2Grafiek4 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem3Grafiek4 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem4Grafiek4 = new List<ItemHistoriek>();
      List<ItemHistoriek> itemhistoriekItem5Grafiek4 = new List<ItemHistoriek>();

      List<double> waardenItem1Grafiek4 = new List<double>();
      List<double> waardenItem2Grafiek4 = new List<double>();
      List<double> waardenItem3Grafiek4 = new List<double>();
      List<double> waardenItem4Grafiek4 = new List<double>();
      List<double> waardenItem5Grafiek4 = new List<double>();

      List<List<double>> alleWaarden = new List<List<double>>();

      List<GrafiekItem> grafiek4GrafiekItems = new List<GrafiekItem>();



      itemhistoriekItem1Grafiek4 = grafiek4items[0].ItemHistorieken;
      grafiek4Legendelijst.Add(grafiek4items[0].Naam);

      grafiek4GrafiekItems.Add(new GrafiekItem()
      {
        ItemId = grafiek4items[0].GemonitordItemId
      });



      itemhistoriekItem2Grafiek4 = grafiek4items[1].ItemHistorieken;
      grafiek4Legendelijst.Add(grafiek4items[1].Naam);

      grafiek4GrafiekItems.Add(new GrafiekItem()
      {
        ItemId = grafiek4items[1].GemonitordItemId
      });



      itemhistoriekItem3Grafiek4 = grafiek4items[2].ItemHistorieken;
      grafiek4Legendelijst.Add(grafiek4items[2].Naam);

      grafiek4GrafiekItems.Add(new GrafiekItem()
      {
        ItemId = grafiek4items[2].GemonitordItemId
      });



      itemhistoriekItem4Grafiek4 = grafiek4items[3].ItemHistorieken;
      grafiek4Legendelijst.Add(grafiek4items[3].Naam);

      grafiek4GrafiekItems.Add(new GrafiekItem()
      {
        ItemId = grafiek4items[3].GemonitordItemId
      });



      itemhistoriekItem5Grafiek4 = grafiek4items[4].ItemHistorieken;
      grafiek4Legendelijst.Add(grafiek4items[4].Naam);

      grafiek4GrafiekItems.Add(new GrafiekItem()
      {
        ItemId = grafiek4items[4].GemonitordItemId
      });

      if (itemhistoriekItem1Grafiek4.Count <= 5)
      {
        aantalGrafiek4 = 0;
      }
      else
      {
        aantalGrafiek4 = itemhistoriekItem1Grafiek4.Count - 5;
      }

      for (int i = aantalGrafiek4; i < itemhistoriekItem1Grafiek4.Count; i++)
      {
        grafiek4XLabels.Add(itemhistoriekItem1Grafiek4[i].HistoriekDatum.ToShortDateString());
        waardenItem1Grafiek4.Add(itemhistoriekItem1Grafiek4[i].AantalVermeldingen);
        waardenItem2Grafiek4.Add(itemhistoriekItem2Grafiek4[i].AantalVermeldingen);
        waardenItem3Grafiek4.Add(itemhistoriekItem3Grafiek4[i].AantalVermeldingen);
        waardenItem4Grafiek4.Add(itemhistoriekItem4Grafiek4[i].AantalVermeldingen);
        waardenItem5Grafiek4.Add(itemhistoriekItem5Grafiek4[i].AantalVermeldingen);

      }

      alleWaarden.Add(waardenItem1Grafiek4);
      alleWaarden.Add(waardenItem2Grafiek4);
      alleWaarden.Add(waardenItem3Grafiek4);
      alleWaarden.Add(waardenItem4Grafiek4);
      alleWaarden.Add(waardenItem5Grafiek4);


      List<Grafiek> grafieken = new List<Grafiek>()
      {

      new Grafiek()
      {
        //GrafiekId = 1,
        //DeelplatformId = 1,
        //DashboardId = 1,
        GrafiekIdNietOpslaan = 1,
        Titel = "Politici - gemiddelde polariteit",
        ToonLegende = false,
        ToonXAs = true,
        ToonYAs = true,

        Type = "bar",

        XOorsprongNul     = true,
        XTitel            = "Items",
        YOorsprongNul     = true,
        YTitel            = "Gemiddelde polariteit",
        XLabels = grafiek1XLabels,
        Datawaarden = new List<List<double>>(){ grafiek1Datawaarden },

        Achtergrondkleur = new List<List<string>>(){ new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850" }, null, null, null, null},
        Randkleur = new List<List<string>>(){ new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850" }, null, null, null, null},
        LegendeLijst = new List<dynamic>{ null, null, null, null, null },


        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiek1Grafiekitems,

        GrafiekWaarde = GrafiekWaarde.Polariteit,

      },
      new Grafiek()
      {
         //GrafiekId = 2,
        //DeelplatformId = 1,
        //DashboardId = 1,
        GrafiekIdNietOpslaan = 2,

        Titel = "Partijen - gemiddelde objectiviteit",
        ToonLegende = true,
        ToonXAs = false,
        ToonYAs = false,

        Type = "pie",

        XOorsprongNul     = true,
        XTitel            = "Items",
        YOorsprongNul     = true,
        YTitel            = "Gemiddelde objectiviteit",
        XLabels = grafiek2XLabels,

        Datawaarden = new List<List<double>>(){ grafiek2Datawaarden},

        Achtergrondkleur = new List<List<string>>(){ new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850", "#ffff00", "#ffa500" }, null, null, null, null},
        Randkleur = new List<List<string>>(){ new List<string> { "#3e95cd", "#8e5ea2", "#3cba9f", "#ff8000", "#c45850", "#ffff00", "#ffa500" }, null, null, null, null},
        LegendeLijst = new List<dynamic>{ null, null, null, null, null },


        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiek2Grafiekitems,
        GrafiekWaarde = GrafiekWaarde.Objectiviteit,
      },

       new Grafiek()
      {
          //GrafiekId = 3,
        //DeelplatformId = 1,
        //DashboardId = 1,
         GrafiekIdNietOpslaan = 3,

        Titel = titelGrafiek3 + " - aantal tweets",
        ToonLegende = false,
        ToonXAs = true,
        ToonYAs = true,

        Type =  "line",

        XOorsprongNul     = true,
        XTitel            = "Dag",
        YOorsprongNul     = true,
        YTitel            = "Aantal tweets",
        XLabels = grafiek3XLabels,
                Periode = 7,

        Datawaarden = new List<List<double>>(){ grafiek3Waarden},

        Achtergrondkleur = new List<List<string>>(){ new List<string> { "#3e95cd" }, null, null, null, null},
        Randkleur = new List<List<string>>(){ new List<string> { "#3e95cd" }, null, null, null, null},
        LegendeLijst = new List<dynamic>{ null, null, null, null, null },

        FillDataset = false,
        Lijnlegendeweergave = false,

        GrafiekItems = grafiek3Grafiekitems,
        GrafiekWaarde = GrafiekWaarde.Vermeldingen,

      },
       new Grafiek()
      {
          //GrafiekId = 4,
        //DeelplatformId = 1,
        //DashboardId = 1,
        GrafiekIdNietOpslaan = 4,

        Titel = "Politici - aantal tweets",
        ToonLegende = true,
        ToonXAs = true,
        ToonYAs = true,

        Type =  "line",

        XOorsprongNul     = true,
        XTitel            = "Dag",
        YOorsprongNul     = true,
        YTitel            = "Aantal tweets",
        XLabels = grafiek4XLabels,
        Periode = 5,

        Datawaarden = alleWaarden,

        Achtergrondkleur = new List<List<string>>(){ new List<string> { "#3e95cd" }, new List<string> {"#8e5ea2" }, new List<string> { "#3cba9f"}, new List<string> { "#ff8000" }, new List<string> { "#c45850" } },
        Randkleur        = new List<List<string>>(){ new List<string> { "#3e95cd" }, new List<string> {"#8e5ea2" }, new List<string> { "#3cba9f"}, new List<string> { "#ff8000" }, new List<string> { "#c45850" } },
        LegendeLijst     = new List<dynamic>{ grafiek4Legendelijst[0], grafiek4Legendelijst[1], grafiek4Legendelijst[2], grafiek4Legendelijst[3], grafiek4Legendelijst[4] },

             FillDataset = false,
        Lijnlegendeweergave = true,

        GrafiekItems = grafiek4GrafiekItems,
        GrafiekWaarde = GrafiekWaarde.Vermeldingen,

      }


    };

      return grafieken;

    }
  }
}


#endregion
