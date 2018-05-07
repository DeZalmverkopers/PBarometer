using BL;
using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public partial class OverzichtController : Controller
    {
        private GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
        // GET: Overzicht
        public virtual ActionResult Index()
        {
            ViewBag.Personen = gemonitordeItemsManager.GetPersonen(1).OrderByDescending(a => a.TotaalAantalVermeldingen);
            ViewBag.Themas = gemonitordeItemsManager.GetThemas(1).OrderByDescending(a => a.TotaalAantalVermeldingen);
            ViewBag.Organisaties = gemonitordeItemsManager.GetOrganisaties(1).OrderByDescending(a => a.TotaalAantalVermeldingen);
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
            ViewBag.Website = persoon.Website != null && persoon.Website.Length > 0;
            
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
            return PartialView("ThemaDetails", ViewBag);
        }
    }
}
        