//Bart
using BL;
using BL.IdentityFramework;
using Domain.Dashboards;
using Domain.Gemonitordeitems;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PB_MVC.Models.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PB_MVC.Controllers
{
    [Authorize]
    public partial class AlertsController : Controller
    {
        //GET: Alerts
        public virtual ActionResult Index()
        {
            AlertManager alertManager = new AlertManager();
            List<Alert> alerts = alertManager.GetAlerts(User.Identity.GetUserId(), true, true).ToList();
            List<AlertViewModel> alertViewModels = new List<AlertViewModel>();

            foreach (var alert in alerts)
            {
                alertViewModels.Add(new AlertViewModel()
                {
                    Beschrijving = alert.Beschrijving,
                    Mail = alert.Mail,
                    Geactiveerd = alert.Geactiveerd,
                    Mobiel = alert.Mobiel,
                    Triggered = alert.Triggered,
                    Onderwerp = alert.GemonitordItem.Naam
                });
            }
            return View(alertViewModels);
        }

        //GET: Create
        [HttpGet]
        public virtual ActionResult Create()
        {
            GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();

            ViewBag.Eigenschappen = new List<string>() { "Polariteit", "Objectiviteit", "Aantal Vermeldingen" }.Select(x => new SelectListItem() { Text = x, Value = x });
            ViewBag.SoortOnderwerp = new List<string>() { "Persoon", "Organisatie", "Thema" }.Select(x => new SelectListItem() { Text = x, Value = x });
            ViewBag.Trend = new List<string>() { "Stijgend", "Dalend", "Neutraal" }.Select(x => new SelectListItem() { Text = x, Value = x });

            List<string> items = gemonitordeItemsManager.GetGemonitordeItems(1).ToList().OrderBy(a => a.Naam).Select(a => a.Naam).ToList();
            var ItemsSelectlist = items.Select(x => new SelectListItem() { Text = x, Value = x });

            ViewBag.Onderwerp = ItemsSelectlist;


            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  virtual ActionResult Create(CreateBasicAlertViewModel createBasicAlertViewModel)
        {
            //TODO DEELPLATFORMID 
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(User.Identity.GetUserId());
            GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
            GemonitordItem gemonitordItem = gemonitordeItemsManager.GetGemonitordItem(1, createBasicAlertViewModel.Onderwerp);
            Alert alert = new Alert()
            {
                Beschrijving = createBasicAlertViewModel.Beschrijving,
                GemonitordItemId = gemonitordItem.GemonitordItemId,
                Mail = createBasicAlertViewModel.Mail,
                Mobiel = createBasicAlertViewModel.Mobiel,
                Geactiveerd = true
            };
            if (user.Alerts == null)
            {
                user.Alerts = new List<Alert>();
            }
            user.Alerts.Add(alert);
            userManager.Update(user);
            return RedirectToAction("Index");
        }

        //GET: Details
        [HttpGet]
        public virtual void Details(Alert alert)
        {
            ViewBag.Details = alert.TriggerRedenen;
            
        }
    }
}