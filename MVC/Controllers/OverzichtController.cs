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
            ViewBag.Facebook = persoon.Facebook != null && persoon.Facebook.Length > 0;
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
            return PartialView("PersoonDetails", ViewBag);
        }
        public ActionResult OrganisatieDetails(int id)
        {
            var organisatie = gemonitordeItemsManager.GetGemonitordItem(id) as Organisatie;
            ViewBag.Organisatie = organisatie;
            ViewBag.Personen = organisatie.Personen;
            ViewBag.GemPolariteit = Math.Round(organisatie.GemPolariteit, 2);
            ViewBag.GemObjectiviteit = Math.Round(organisatie.GemObjectiviteit, 2);
            return PartialView("OrganisatieDetails", ViewBag);
        }
    }
}
        