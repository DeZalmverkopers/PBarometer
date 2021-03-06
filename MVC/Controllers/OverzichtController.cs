﻿using BL;
using Domain.Deelplatformen;
using Domain.Gemonitordeitems;
using MVC.Models.Overzicht;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC.Controllers
{
  public partial class OverzichtController : Controller
  {
    private GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
    // GET: Overzicht

    public Deelplatform HuidigDeelplatform
    {
      get
      {
        return new DeelplatformenManager().GetDeelplatformByURL(RouteData.Values["deelplatform"].ToString());
      }
    }
    public virtual ActionResult Index()
    {
      if (HuidigDeelplatform == null)
      {
        return RedirectToAction("Index", "Home");
      }
      ViewBag.Personen = gemonitordeItemsManager.GetPersonen(HuidigDeelplatform.DeelplatformId).Cast<Persoon>().OrderByDescending(a => a.TotaalAantalVermeldingen);
      ViewBag.Themas = gemonitordeItemsManager.GetThemas(HuidigDeelplatform.DeelplatformId).OrderByDescending(a => a.TotaalAantalVermeldingen);
      ViewBag.Organisaties = gemonitordeItemsManager.GetOrganisaties(HuidigDeelplatform.DeelplatformId).OrderByDescending(a => a.TotaalAantalVermeldingen);
      ViewBag.Gemeentes = gemonitordeItemsManager.GetPersonen(HuidigDeelplatform.DeelplatformId).Cast<Persoon>().Where(a => a.Gemeente != null && !a.Gemeente.Equals("")).Select(a => a.Gemeente.ToUpper()).Distinct().OrderBy(a => a).ToList();
      return View();
    }
    public virtual ActionResult PersoonDetails(int id)
    {

      var persoon = gemonitordeItemsManager.GetPersoon(id, true) as Persoon;
      ViewBag.Persoon = persoon;
      ViewBag.HeeftFacebook = persoon.Facebook != null && persoon.Facebook.Length > 0;
      ViewBag.Twitter = persoon.TwitterHandle != null && persoon.TwitterHandle.Length > 0;
      if (persoon.Geboortedatum.HasValue)
      {
        ViewBag.Geboortedatum = persoon.Geboortedatum.Value.ToString("d");
      }
      else
      {
        ViewBag.Geboortedatum = "Onbekend";
      }
      ViewBag.Website = false;
      if (persoon.Website != null && persoon.Website.Length > 0)
      {
        ViewBag.Website = true;
        if (!persoon.Website.Substring(0, 4).Equals("http"))
        {
          ViewBag.WebsiteURL = "http://" + persoon.Website;
        }
        else
        {
          ViewBag.WebsiteURL = persoon.Website;
        }
      }
      ViewBag.GemPolariteit = Math.Round(persoon.GemPolariteit, 2);
      ViewBag.GemObjectiviteit = Math.Round(persoon.GemObjectiviteit, 2);
      if (persoon.Organisatie != null)
      {
        ViewBag.Organisatie = persoon.Organisatie.Naam;
      }
      else
      {
        ViewBag.Organisatie = "Onbekend";
      }
      if (persoon.Gemeente != null && persoon.Gemeente.Length > 0)
      {
        ViewBag.Gemeente = persoon.Gemeente.First().ToString().ToUpper() + persoon.Gemeente.Substring(1).ToLower();
      }
      else
      {
        ViewBag.Gemeente = "Onbekend";
      }
      if (persoon.Postcode != null && persoon.Postcode.Length > 0)
      {
        ViewBag.Postcode = persoon.Postcode;
      }
      else
      {
        ViewBag.Postcode = "Onbekend";
      }
      if (persoon.MeestVoorkomendeURL != null && persoon.MeestVoorkomendeURL.Length > 0)
      {
        ViewBag.HeeftMeestVoorkomendeURL = true;
        ViewBag.MeestVoorkomendeURL = persoon.MeestVoorkomendeURL;
        if (persoon.MeestVoorkomendeURL.Length > 20)
        {
          ViewBag.MeestVoorkomendeURL = persoon.MeestVoorkomendeURL.Substring(0, 20) + "...";
        }
      }
      else
      {
        ViewBag.HeeftMeestVoorkomendeURL = false;
      }

      ViewBag.PersoonId = persoon.GemonitordItemId;
      ViewBag.PersoonNaam = persoon.Naam;
      ViewBag.ItemDagen = persoon.ItemHistorieken.Select(a => a.HistoriekDatum.ToShortDateString()).ToList();
      ViewBag.DagenLengte = persoon.ItemHistorieken.Select(a => a.HistoriekDatum.ToShortDateString()).ToList().Count;


      ViewBag.ItemAantalTweets = persoon.ItemHistorieken.Select(a => a.AantalVermeldingen).ToList();

      return PartialView("PersoonDetails", ViewBag);
    }


    public ActionResult OrganisatieDetails(int id)
    {
      var organisatie = gemonitordeItemsManager.GetGemonitordItem(id) as Organisatie;
      ViewBag.Organisatie = organisatie;
      ViewBag.Personen = organisatie.Personen.OrderByDescending(a => a.TotaalAantalVermeldingen);
      ViewBag.GemPolariteit = Math.Round(organisatie.GemPolariteit, 2);
      ViewBag.GemObjectiviteit = Math.Round(organisatie.GemObjectiviteit, 2);
      if (organisatie.MeestVoorkomendeURL != null && organisatie.MeestVoorkomendeURL.Length > 0)
      {
        ViewBag.HeeftMeestVoorkomendeURL = true;
        ViewBag.MeestVoorkomendeURL = organisatie.MeestVoorkomendeURL;
        if (organisatie.MeestVoorkomendeURL.Length > 20)
        {
          ViewBag.MeestVoorkomendeURL = organisatie.MeestVoorkomendeURL.Substring(0, 20) + "...";
        }
      }
      else
      {
        ViewBag.HeeftMeestVoorkomendeURL = false;
      }

      ViewBag.ItemDagen = organisatie.ItemHistorieken.Select(a => a.HistoriekDatum.ToShortDateString()).ToList();
      ViewBag.ItemAantalTweets = organisatie.ItemHistorieken.Select(a => a.AantalVermeldingen).ToList();

      ViewBag.OrganisatieId = organisatie.GemonitordItemId;
      ViewBag.OrganisatieNaam = organisatie.Naam;
      ViewBag.ItemDagen = organisatie.ItemHistorieken.Select(a => a.HistoriekDatum.ToShortDateString()).ToList();
      ViewBag.DagenLengte = organisatie.ItemHistorieken.Select(a => a.HistoriekDatum.ToShortDateString()).ToList().Count;

      return PartialView("OrganisatieDetails", ViewBag);
    }


    public ActionResult ThemaDetails(int id)

    {
      var thema = gemonitordeItemsManager.GetGemonitordItem(id) as Thema;
      ViewBag.Thema = thema;
      string kernwoorden = "";
      foreach (string item in thema.KernWoorden.OrderBy(a => a))
      {
        kernwoorden += item + ", ";
      }
      ViewBag.Kernwoorden = kernwoorden.Substring(0, kernwoorden.Length - 2);
      ViewBag.GemPolariteit = Math.Round(thema.GemPolariteit, 2);
      ViewBag.GemObjectiviteit = Math.Round(thema.GemObjectiviteit, 2);
      if (thema.MeestVoorkomendeURL != null && thema.MeestVoorkomendeURL.Length > 0)
      {
        ViewBag.HeeftMeestVoorkomendeURL = true;
        ViewBag.MeestVoorkomendeURL = thema.MeestVoorkomendeURL;
        if (thema.MeestVoorkomendeURL.Length > 20)
        {
          ViewBag.MeestVoorkomendeURL = thema.MeestVoorkomendeURL.Substring(0, 20) + "...";
        }
      }
      else
      {
        ViewBag.HeeftMeestVoorkomendeURL = false;
      }

      ViewBag.ItemDagen = thema.ItemHistorieken.Select(a => a.HistoriekDatum.ToShortDateString()).ToList();
      ViewBag.ItemAantalTweets = thema.ItemHistorieken.Select(a => a.AantalVermeldingen).ToList();

      ViewBag.ThemaId = thema.GemonitordItemId;
      ViewBag.ThemaNaam = thema.Naam;
      ViewBag.ItemDagen = thema.ItemHistorieken.Select(a => a.HistoriekDatum.ToShortDateString()).ToList();
      ViewBag.DagenLengte = thema.ItemHistorieken.Select(a => a.HistoriekDatum.ToShortDateString()).ToList().Count;

      return PartialView("ThemaDetails", ViewBag);
    }




    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult MaakPersoon()
    {
      return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public ActionResult MaakPersoon(PersoonViewModel maakPersoonViewModel)
    {
      if (ModelState.IsValid)
      {
        string naamOrg = (maakPersoonViewModel.NaamOrganisatie ?? "Onafhankelijk");
        Organisatie organisatie = gemonitordeItemsManager.GetOrganisatie(naamOrg);

        if (organisatie == null)
        {
          gemonitordeItemsManager.AddGemonitordItem(new Organisatie()
          {
            Naam = naamOrg,
            DeelplatformId = HuidigDeelplatform.DeelplatformId
          });
          organisatie = gemonitordeItemsManager.GetOrganisatie(naamOrg);
        }
        Persoon persoon = new Persoon
        {
          Naam = maakPersoonViewModel.Naam,
          Organisatie = organisatie,
          Gemeente = maakPersoonViewModel.Gemeente,
          Geboortedatum = maakPersoonViewModel.Geboortedatum,
          Facebook = maakPersoonViewModel.Facebook,
          DeelplatformId = HuidigDeelplatform.DeelplatformId,
          Postcode = maakPersoonViewModel.Postcode,
          TwitterHandle = maakPersoonViewModel.TwitterHandle,
          Website = maakPersoonViewModel.Website,
        };
        gemonitordeItemsManager.AddGemonitordItem(persoon);

        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }


    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult MaakThema()
    {
      return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public ActionResult MaakThema(ThemaViewModel maakThemaViewModel)
    {
      if (ModelState.IsValid)
      {
        string naam = maakThemaViewModel.Naam;
        List<string> kernwoorden = maakThemaViewModel.Kernwoorden.Split(',').Where(a => !a.Equals("")).ToList();
        gemonitordeItemsManager.AddThema(naam, kernwoorden, HuidigDeelplatform.DeelplatformId);
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult MaakOrganisatie()
    {
      return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult MaakOrganisatie(OrganisatieViewModel maakOrganisatieViewModel)
    {
      if (ModelState.IsValid)
      {
        List<string> leden = maakOrganisatieViewModel.Leden.Split(',').Select(a => a.Trim()).Where(a => !a.Equals("")).ToList();
        gemonitordeItemsManager.AddOrganisatie(maakOrganisatieViewModel.Naam, HuidigDeelplatform.DeelplatformId, leden);
        return RedirectToAction("Index");
      }
      else return View();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult PasPersoonAan(int id)
    {
      Persoon persoon = gemonitordeItemsManager.GetPersoon(id, true);
      string naamOrganisatie = persoon.Organisatie != null ? persoon.Organisatie.Naam : "";
      PersoonViewModel persoonViewModel = new PersoonViewModel()
      {
        Facebook = persoon.Facebook,
        Naam = persoon.Naam,
        Gemeente = persoon.Gemeente,
        Postcode = persoon.Postcode,
        Geboortedatum = persoon.Geboortedatum,
        NaamOrganisatie = naamOrganisatie,
        TwitterHandle = persoon.TwitterHandle,
        Website = persoon.Website,
        Id = id
      };
      return View(persoonViewModel);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult PasPersoonAan(PersoonViewModel persoonViewModel)
    {
      if (ModelState.IsValid)
      {
        Persoon persoon = gemonitordeItemsManager.GetPersoon(persoonViewModel.Id, true);
        Organisatie nieuweOrganisatie = gemonitordeItemsManager.GetOrganisatie(persoonViewModel.NaamOrganisatie);
        Organisatie oudeOrganisatie = persoon.Organisatie != null ? gemonitordeItemsManager.GetGemonitordItem(persoon.Organisatie.GemonitordItemId) as Organisatie : null;
        bool organisatieToegevoegd = false;
        bool organisatieVeranderd = false;

        if (nieuweOrganisatie == null && persoonViewModel.NaamOrganisatie != null)
        {
          gemonitordeItemsManager.AddOrganisatie(persoonViewModel.NaamOrganisatie, HuidigDeelplatform.DeelplatformId, new List<string>() { persoonViewModel.Naam });
          organisatieToegevoegd = true;
        }
        if (persoon.Organisatie != null && !persoon.Organisatie.Naam.Equals(persoonViewModel.Naam))
        {
          organisatieVeranderd = true;
        }
        persoon.TwitterHandle = persoonViewModel.TwitterHandle;
        persoon.Facebook = persoonViewModel.Facebook;
        persoon.Geboortedatum = persoonViewModel.Geboortedatum;
        persoon.Gemeente = persoonViewModel.Gemeente;
        persoon.Postcode = persoonViewModel.Postcode;
        persoon.Naam = persoonViewModel.Naam;
        persoon.Website = persoonViewModel.Website;
        persoon.Organisatie = gemonitordeItemsManager.GetOrganisatie(persoonViewModel.NaamOrganisatie);
        if (!organisatieToegevoegd && organisatieVeranderd)
        {
          gemonitordeItemsManager.BepaalDetailItemsVoorOrganisatie(nieuweOrganisatie);
          gemonitordeItemsManager.BerekenEigenschappen(nieuweOrganisatie);
          gemonitordeItemsManager.ChangeGemonitordItem(nieuweOrganisatie);
        }
        if (oudeOrganisatie != null)
        {
          gemonitordeItemsManager.BepaalDetailItemsVoorOrganisatie(oudeOrganisatie);
          gemonitordeItemsManager.BerekenEigenschappen(oudeOrganisatie);
          gemonitordeItemsManager.ChangeGemonitordItem(oudeOrganisatie);
        }
        gemonitordeItemsManager.ChangeGemonitordItem(persoon);
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }

    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult PasThemaAan(int id)
    {
      Thema thema = gemonitordeItemsManager.GetGemonitordItem(id) as Thema;
      ThemaViewModel themaViewModel = new ThemaViewModel()
      {
        Kernwoorden = String.Join(", ", thema.KernWoorden.ToArray()),
        Naam = thema.Naam,
        Id = thema.GemonitordItemId
      };
      return View(themaViewModel);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult PasThemaAan(ThemaViewModel themaViewModel)
    {
      if (ModelState.IsValid)
      {
        Thema thema = gemonitordeItemsManager.GetGemonitordItem(themaViewModel.Id) as Thema;
        thema.Naam = themaViewModel.Naam;
        thema.KernWoorden = themaViewModel.Kernwoorden.Split(',').Select(a => a.Trim()).Where(a => !a.Equals("")).ToList();
        gemonitordeItemsManager.ChangeGemonitordItem(thema);

        return RedirectToAction("Index");
      }


      return View();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult PasOrganisatieAan(int id)
    {
      Organisatie organisatie = gemonitordeItemsManager.GetGemonitordItem(id) as Organisatie;
      List<string> leden = organisatie.Personen.Select(a => a.Naam).ToList();
      OrganisatieViewModel organisatieViewModel = new OrganisatieViewModel()
      {
        Leden = String.Join(", ", leden),
        Naam = organisatie.Naam,
        Id = organisatie.GemonitordItemId
      };
      return View(organisatieViewModel);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult PasOrganisatieAan(OrganisatieViewModel organisatieViewModel)
    {
      if (ModelState.IsValid)
      {
        Organisatie organisatie = gemonitordeItemsManager.GetGemonitordItem(organisatieViewModel.Id) as Organisatie;
        gemonitordeItemsManager.EditOrganisatie(organisatieViewModel.Id, HuidigDeelplatform.DeelplatformId, organisatieViewModel.Naam,
            organisatieViewModel.Leden.Split(',').Select(a => a.Trim()).Where(a => !a.Equals("")).ToList());
        return RedirectToAction("Index");
      }
      else
      {
        return View(organisatieViewModel);
      }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult VerwijderItem(int id)
    {
      gemonitordeItemsManager.RemoveGemonitordItem(id);
      return RedirectToAction("Index");
    }
  }
}
