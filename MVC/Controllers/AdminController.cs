using BL;
using Domain.Deelplatformen;
using Domain.Gemonitordeitems;
using MVC.Models;
using MVC.Models.Specifieke_Pagina;
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
    public partial class AdminController : Controller
    {
        DeelplatformenManager manager = new DeelplatformenManager();

        public Deelplatform HuidigDeelplatform
        {
            get
            {
                return manager.GetDeelplatformByURL(RouteData.Values["deelplatform"].ToString());
            }
        }

        // GET: Admin
        public virtual ActionResult Index()
        {
            if (HuidigDeelplatform == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
                int deelplatformId = HuidigDeelplatform.DeelplatformId;
                GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
                string dataUitBestand = new StreamReader(bestand.InputStream).ReadToEnd();
                var personen = JArray.Parse(dataUitBestand);
                foreach (var persoon in personen)
                {
                    List<GemonitordItem> organisaties = gemonitordeItemsManager.GetOrganisaties(deelplatformId).ToList();
                    var persoonDict = persoon.ToObject<Dictionary<string, dynamic>>();
                    var personenUitDb = gemonitordeItemsManager.GetPersonen(deelplatformId);
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
                            DeelplatformId = deelplatformId,
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
                                DeelplatformId = deelplatformId
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

        [HttpGet]
        public virtual ActionResult LayoutAanpassen()
        {
            LayoutViewModel model = new LayoutViewModel
            {
                Kleur = HuidigDeelplatform.Achtergrondkleur
            };
            return PartialView(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LayoutAanpassen(LayoutViewModel layoutViewModel)
        {
            HuidigDeelplatform.Achtergrondkleur = layoutViewModel.Kleur;

            if (layoutViewModel.Afbeelding != null)
            {
                string afbeelding = layoutViewModel.Afbeelding.FileName;
                string extensie = Path.GetExtension(afbeelding);
                if (extensie.Equals(".png") || extensie.Equals(".jpeg") || extensie.Equals(".jpg") || extensie.Equals(".gif"))
                {
                    layoutViewModel.Afbeelding.SaveAs(Path.Combine(Server.MapPath("~/images/Deelplatformen"), HuidigDeelplatform.URLnaam + extensie));
                    Deelplatform deelplatform = HuidigDeelplatform;
                    deelplatform.AfbeeldingPad = HuidigDeelplatform.URLnaam + extensie;
                    DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
                    deelplatformenManager.ChangeDeelplatform(deelplatform);
                }
            }
            return RedirectToAction("Index", "Dashboard");
        }

        //Slaat de huidige instelling van de achtergrond op.
        [HttpGet]
        public virtual ActionResult SlaAchtergrondOp(string kleur)
        {
            manager.ChangeAchtergrondkleur(HuidigDeelplatform.DeelplatformId, kleur);
            return PartialView("~/Views/Shared/AdminSuperadmin/LayoutAanpassen.cshtml");
        }

        //Opent de partialview om in te stellen wat niet-ingelogde gebruikers al dan niet
        //kunnen zien en doen op het huidige deelplatform.
        public virtual ActionResult LaadNietIngelogd()
        {
            SettingsNotLoggedInViewModel model = new SettingsNotLoggedInViewModel();
            Settings settings = manager.GetSettings(HuidigDeelplatform.DeelplatformId);
            model.OverzichtAdded = settings.OverzichtAdded;
            model.AlertsAdded = settings.AlertsAdded;
            return PartialView("~/Views/Shared/AdminSuperadmin/NietIngelogdeGebruikerInstellen.cshtml", model);
        }

        //Slaat op of de niet-ingelogde gebruikers al dan niet het overzicht kunnen raadplegen.
        [HttpGet]
        public virtual ActionResult SlaOverzichtAddedOp(bool OverzichtAdded)
        {
            ViewBag.OverzichtAdded = OverzichtAdded;
            manager.ChangeOverzichtAdded(HuidigDeelplatform.DeelplatformId, OverzichtAdded);
            return RedirectToAction("Index", User.IsInRole("SuperAdmin") ? "Superadmin" : "Admin");
        }

        //Slaat op of de niet-ingelogde gebruikers al dan niet de alerts kunnen raadplegen.
        [HttpGet]
        public virtual ActionResult SlaAlertsAddedOp(bool AlertsAdded)
        {
            ViewBag.AlertsAdded = AlertsAdded;
            manager.ChangeAlertsAdded(HuidigDeelplatform.DeelplatformId, AlertsAdded);
            return RedirectToAction("Index", User.IsInRole("SuperAdmin") ? "Superadmin" : "Admin");
        }

        //Opent de partialview waarin de admin FAQ Items kan verwijderen.
        [HttpGet]
        public ActionResult LaadFAQItemVerwijderen()
        {
            List<FAQViewModel> models = new List<FAQViewModel>();
            List<FAQItem> FAQItems = manager.GetFAQItems(HuidigDeelplatform.DeelplatformId);
            if (FAQItems != null)
            {
                foreach (var FAQItem in FAQItems)
                {
                    models.Add(new FAQViewModel() { Vraag = FAQItem.Vraag });
                }
            }
            return PartialView("~/Views/Shared/AdminSuperadmin/LaadFAQItemVerwijderen.cshtml", models);
        }

        //Verwijdert een FAQ Item.
        [HttpGet]
        public ActionResult VerwijderFAQItem(string vraag)
        {
            if (ModelState.IsValid)
            {
                manager.RemoveFAQItem(HuidigDeelplatform.DeelplatformId, vraag);
            }
            return RedirectToAction("Index", User.IsInRole("SuperAdmin") ? "Superadmin" : "Admin");
        }

        //Opent de partialview waarin de admin FAQ Items kan toevoegen.
        [HttpGet]
        public ActionResult LaadFAQItemToevoegen()
        {
            return PartialView("~/Views/Shared/AdminSuperadmin/LaadFAQItemToevoegen.cshtml");
        }

        //Slaat nieuw FAQ Item op.
        [HttpPost]
        public ActionResult LaadFAQItemToevoegen(FAQViewModel model)
        {
            if (ModelState.IsValid)
            {
                manager.AddNieuweFAQItem(HuidigDeelplatform.DeelplatformId, new FAQItem() { Vraag = model.Vraag, Antwoord = model.Antwoord });
            }
            return RedirectToAction("Index", User.IsInRole("SuperAdmin") ? "Superadmin" : "Admin");
        }
    }
}