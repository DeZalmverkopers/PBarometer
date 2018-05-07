using BL;
using Domain.Deelplatformen;
using Domain.Gemonitordeitems;
using MVC.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
  [Authorize(Roles = "Admin")]
  [RequireHttps]
  public partial class AdminController : Controller
  {
    // GET: Admin
    public virtual ActionResult Index()
    {
      return View();
    }

    public virtual ActionResult LaadGemonitordeItemsBeheren()
    {
      return PartialView("~/Views/Shared/AdminSuperadmin/GemonitordeItemsBeheren.cshtml");
    }

    [HttpGet]
    public virtual ActionResult LaadData()
    {
      return PartialView("~/Views/Shared/AdminSuperadmin/DataImporterenEnExporteren.cshtml");
    }

    [HttpPost]
    public virtual ActionResult LaadData(HttpPostedFileBase bestand)
    {
      if (Path.GetExtension(bestand.FileName).Equals(".json"))
      {
        GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
        string dataUitBestand = new StreamReader(bestand.InputStream).ReadToEnd();
        var personen = JArray.Parse(dataUitBestand);
        foreach (var persoon in personen)
        {
          List<GemonitordItem> organisaties = gemonitordeItemsManager.GetOrganisaties(1).ToList();
          var persoonDict = persoon.ToObject<Dictionary<string, dynamic>>();
          var personenUitDb = gemonitordeItemsManager.GetPersonen(1);
          if (personenUitDb.FirstOrDefault(a => a.Naam.Equals(persoonDict["full_name"])) == null)
          {
            DateTime.TryParse(persoonDict["dateOfBirth"], out DateTime geboorteDatum);
            Persoon toeTeVoegenPersoon = new Persoon()
            {
              Naam = persoonDict["full_name"],
              TwitterHandle = persoonDict["twitter"],
              Website = persoonDict["site"],
              Postcode = persoonDict["postal_code"],
              Gemeente = persoonDict["town"],
              Facebook = persoonDict["facebook"],
              Volgbaar = true,
              DeelplatformId = 1,
              Geboortedatum = geboorteDatum
            };
            Organisatie organisatie = organisaties.FirstOrDefault(a => a.Naam.Equals(persoonDict["organisation"])) as Organisatie;
            if (organisatie != null)
            {
              organisatie.Personen.Add(toeTeVoegenPersoon);
              gemonitordeItemsManager.ChangeGemonitordItem(organisatie);
            }
            else
            {
              organisatie = new Organisatie()
              {
                Naam = persoonDict["organisation"],
                DeelplatformId = 1
              };
              organisatie.Personen.Add(toeTeVoegenPersoon);
              gemonitordeItemsManager.AddGemonitordItem(organisatie);
            }
          }
        }
        return RedirectToAction("Index");
      }
      return RedirectToAction("Index");
    }

    public virtual ActionResult LaadGebruikersactiviteit()
    {
      return PartialView("~/Views/Shared/AdminSuperadmin/GebruikersActiviteitMonitoren.cshtml");
    }

    public virtual ActionResult LaadLayout()
    {
      return PartialView("~/Views/Shared/AdminSuperadmin/LayoutAanpassen.cshtml");
    }

    public virtual ActionResult LaadNietIngelogd()
    {
      SettingsNotLoggedInViewModel model = new SettingsNotLoggedInViewModel();
      Settings settings = new DeelplatformenManager().GetSettings();
      model.OverzichtAdded = settings.OverzichtAdded;
      model.WeeklyReviewAdded = settings.WeeklyReviewAdded;
      return PartialView("~/Views/Shared/AdminSuperadmin/NietIngelogdeGebruikerInstellen.cshtml", model);
    }

    [HttpGet]
    public virtual ActionResult SlaInstellingenOp(bool OverzichtAdded, bool WeeklyReviewAdded)
    {
      ViewBag.OverzichtAdded = OverzichtAdded;
      ViewBag.WeeklyReviewAdded = WeeklyReviewAdded;
      new DeelplatformenManager().ChangeSettings(new Settings(OverzichtAdded, WeeklyReviewAdded));
      string controller = User.IsInRole("SuperAdmin") ? "SuperAdmin" : "Admin";
      return RedirectToAction("Index", controller);
    }

    [HttpGet]
    public virtual ActionResult SlaAchtergrondOp(string kleur)
    {
      new DeelplatformenManager().ChangeAchtergrond(kleur);
      string controller = User.IsInRole("SuperAdmin") ? "SuperAdmin" : "Admin";
      return RedirectToAction("Index", controller);
    }
  }
}