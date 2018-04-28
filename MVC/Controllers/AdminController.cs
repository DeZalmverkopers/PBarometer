using BL;
using BL.IdentityFramework;
using Domain.Gemonitordeitems;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using MVC.Models;
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
            List<ApplicationUser> users = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
            List<AdminUserViewModel> userViewModels = new List<AdminUserViewModel>();
            foreach (var user in users)
            {
                userViewModels.Add(new AdminUserViewModel
                {
                    Email = user.Email,
                }
                );
            }
            return View(userViewModels);
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

    }
}