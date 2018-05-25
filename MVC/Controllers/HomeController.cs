using System.Collections.Generic;
using System.Web.Mvc;
using BL;
using MVC.Controllers.Api;
using Domain.Dashboards;
using Domain.Gemonitordeitems;
using System.Linq;
using Domain.Deelplatformen;
using Microsoft.AspNet.Identity;
using MVC.Models.Home;

namespace MVC.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            if (RouteData.Values["deelplatform"] == null)
            {
                ViewBag.GeenDeelplatformGeselecteerd = true;
            }
            DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
            List<Deelplatform> deelplatformen = deelplatformenManager.GetDeelplatformen().ToList();
            List<DeelplatformViewModel> deelplatformViewModels = new List<DeelplatformViewModel>();
            foreach (var deelplatform in deelplatformen)
            {
                string afbeeldingPad = deelplatform.AfbeeldingPad;
                if (afbeeldingPad == null)
                {
                    afbeeldingPad = "default.png";
                }
                deelplatformViewModels.Add(new DeelplatformViewModel()
                {
                    Naam = deelplatform.Naam,
                    Afbeelding = afbeeldingPad,
                    URL = deelplatform.URLnaam
                });
            }
            return View(deelplatformViewModels);
        }

        public ActionResult Error() {
            return View("~/Shared/Error");
        }
    }
}