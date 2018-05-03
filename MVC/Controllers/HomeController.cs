using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BL;
using MVC.Controllers.Api;
using MVC.Models;


namespace MVC.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult AddItems()
        {
            DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
            deelplatformenManager.AddDeelplatform(new Domain.Deelplatformen.Deelplatform { LaatsteSynchronisatie = DateTime.Now.AddDays(-20), Naam = "Politieke Barometer" , AantalDagenHistoriek = 10});
            int id = deelplatformenManager.GetDeelplatformByName("Politieke Barometer").DeelplatformId;
            GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
            gemonitordeItemsManager.AddOrganisatie("Open VLD", id, new List<string>() { "Alexander De Croo", "Gwendolyn Rutten", "Maggie De Block" });
            gemonitordeItemsManager.AddOrganisatie("Groen", id, new List<string>() { "Kristof Calvo", "Meyrem Almaci", "Wouter Van Besien" });
            gemonitordeItemsManager.AddOrganisatie("SPA", id, new List<string>() { "Caroline Gennez", "John Crombez", "Bruno Tobback" });
            gemonitordeItemsManager.AddOrganisatie("Vlaams Belang", id, new List<string>() { "Filip Dewinter", "Tom Van Grieken", "Gerolf Annemans" });

            gemonitordeItemsManager.AddThema("Migratie", new List<string>() { "buitenland", "vluchteling", "immigratie", "migratie" }, id);
            gemonitordeItemsManager.AddThema("Fiscaliteit", new List<string>() { "belastingen", "tax", "btw", "sociale zekerheid" }, id);
            gemonitordeItemsManager.AddThema("Milieu", new List<string>() { "kernenergie", "zonnenergie", "steenkool", "luchtvervuiling", "windenergie" }, id);

            return RedirectToAction("Index");
        }

        public virtual ActionResult GetData()
        {
            DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
            TextgainController textgainController = new TextgainController();
            textgainController.HaalBerichtenOp(deelplatformenManager.GetDeelplatformByName("Politieke Barometer"));

            return RedirectToAction("Index");
        }
    }
}