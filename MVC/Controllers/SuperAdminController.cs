using BL;
using BL.IdentityFramework;
using Domain.Dashboards;
using Domain.Deelplatformen;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVC.Models;
using MVC.Models.SuperAdmin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public partial class SuperAdminController : Controller
    {
        private List<ApplicationUser> users
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
            }
        }

        public DeelplatformenManager deelplatformenManager = new DeelplatformenManager();

        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public Deelplatform HuidigDeelplatform
        {
            get
            {
                return deelplatformenManager.GetDeelplatformByURL(RouteData.Values["deelplatform"].ToString());
            }
        }

        // GET: SuperAdmin
        public virtual ActionResult Index()
        {
            if (RouteData.Values["deelplatform"] == null)
            {
                ViewBag.GeenDeelplatformGeselecteerd = true;
            }
            return View();
        }


        public virtual ActionResult LaadData()
        {
            return PartialView("~/Views/Shared/AdminSuperadmin/DataImporterenEnExporteren.cshtml");
        }

        public virtual ActionResult LaadGebruikersactiviteit()
        {
            return PartialView("~/Views/Shared/AdminSuperadmin/GebruikersactiviteitMonitoren.cshtml");
        }

        public virtual ActionResult LaadLayout()
        {
            return PartialView("~/Views/Shared/AdminSuperadmin/LayoutAanpassen.cshtml");
        }


        public virtual ActionResult LaadGebruikersgegevens()
        {
            List<GebruikersViewModel> models = new List<GebruikersViewModel>();

            List<ApplicationUser> regularUsersAndAdmins = UserManager.GetRegularUsersAndAdmins(users);

            foreach (ApplicationUser user in regularUsersAndAdmins)
            {
                models.Add(new GebruikersViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
            }
            return PartialView("~/Views/Shared/Superadmin/GebruikersgegevensNakijken.cshtml", models);
        }

        [HttpGet]
        public virtual async Task<ActionResult> VerwijderGebruiker(string Email)
        {
            ApplicationUser user = users.Find(u => u.Email.Equals(Email));
            var logins = user.Logins;
            var rolesForUser = UserManager.GetRoles(user.Id);

            foreach (var login in logins.ToList())
            {
                await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            }

            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser.ToList())
                {
                    // item should be the name of the role
                    var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                }
            }

            UserManager.DeleteUser(user);
            return RedirectToAction("Index");
        }

        public virtual ActionResult LaadDeelplatform()
        {
            return PartialView("~/Views/Shared/Superadmin/DeelplatformAanmaken.cshtml");
        }

        public virtual ActionResult LaadGebruikers()
        {
            List<SuperAdminUserViewModel> userViewModels = new List<SuperAdminUserViewModel>();
            List<ApplicationUser> regularUsersAndAdmins = UserManager.GetRegularUsersAndAdmins(users);

            foreach (ApplicationUser user in regularUsersAndAdmins)
            {
                var roles = UserManager.GetRoles(user.Id);
                bool isAdmin = false;
                foreach (string role in roles)
                {
                    if (role.Equals("Admin"))
                    {
                        isAdmin = true;
                    }
                }
                userViewModels.Add(new SuperAdminUserViewModel
                {
                    IsAdmin = isAdmin,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                }
                  );
            }
            return PartialView("~/Views/Shared/AdminSuperadmin/LaadGebruikers.cshtml", userViewModels);
        }

        [HttpGet]
        public virtual ActionResult SlaAdminOp(string email, bool setAdmin)
        {
            Claim claim = new Claim("DeelplatformId", HuidigDeelplatform.DeelplatformId.ToString());

            ApplicationRoleManager roleManager = new ApplicationRoleManager();

            ApplicationUser user = users.Find(u => u.Email.Equals(email));
            var roles = UserManager.GetRoles(user.Id);
            bool isAdmin = false;
            foreach (string role in roles)
            {
                if (role.Equals("Admin"))
                {
                    isAdmin = true;
                }
            }
            var adminRole = roleManager.FindByName("Admin");
            if (!isAdmin && setAdmin)
            {
                UserManager.AddToRole(user.Id, adminRole.Name);
                UserManager.AddClaim(user.Id, claim);
            }
            else if (isAdmin && !setAdmin)
            {
                UserManager.RemoveFromRole(user.Id, adminRole.Name);
                UserManager.RemoveClaim(user.Id, claim);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult DeelplatformOverzicht()
        {
            return PartialView("OverzichtDeelplatform", deelplatformenManager.GetDeelplatformen().Select(a => new DeelplatformOverzichtViewModel() { Id = a.DeelplatformId, Naam = a.Naam, URL = a.URLnaam }));
        }

        [HttpGet]
        public ActionResult MaakDeelplatform()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaakDeelplatform(MaakDeelplatformViewModel maakDeelplatformViewModel)
        {
            if (ModelState.IsValid)
            {
                if (deelplatformenManager.GetDeelplatformByURL(maakDeelplatformViewModel.URLNaam) != null)
                {
                    ViewBag.Boodschap = "Deze URL is al in gebruik";
                    return PartialView();
                }

                //Wanneer de URL van een deelplatform overeenkomt met die van een controller, loopt de routing mis. Dit wordt hier vermeden
                List<string> controllerNamen = new List<string>() { "Alerts", "Android","IData","Textgain","Account","Admin",
                    "Dashboard", "DashboardStatistieken","GemonitordItem","Overzicht","Profiel","SpecifiekePagina","SuperAdmin" };
                if (controllerNamen.Any(a => a.Equals(maakDeelplatformViewModel.URLNaam, StringComparison.OrdinalIgnoreCase)))
                {
                    ViewBag.Boodschap = "Deze URL is niet geldig";
                    return PartialView();
                }
                deelplatformenManager.AddDeelplatform(new Deelplatform()
                {
                    AantalDagenHistoriek = maakDeelplatformViewModel.AantalDagenHistoriek,
                    URLnaam = maakDeelplatformViewModel.URLNaam,
                    LaatsteSynchronisatie = DateTime.Now.AddYears(-100),
                    Naam = maakDeelplatformViewModel.Naam,
                    DataOphaalFrequentie = maakDeelplatformViewModel.DataOphaalFrequentie
                });
                List<Deelplatform> deelplatformen = deelplatformenManager.GetDeelplatformen().ToList(); ;
                foreach (var user in UserManager.Users.ToList())
                {
                    foreach (var deelplatform in deelplatformen)
                    {
                        user.Dashboards.Add(new Dashboard() { DeelplatformId = deelplatform.DeelplatformId });
                    }
                    UserManager.Update(user);
                }
                ViewBag.Boodschap = "Het deelplatform is aangemaakt";
                return PartialView();
            }
            return PartialView(maakDeelplatformViewModel);
        }
        [HttpGet]
        public ActionResult EditDeelplatform(int id)
        {
            Deelplatform deelplatform = deelplatformenManager.GetDeelplatform(id);
            MaakDeelplatformViewModel maakDeelplatformViewModel = new MaakDeelplatformViewModel()
            {
                Id = id,
                AantalDagenHistoriek = deelplatform.AantalDagenHistoriek,
                Naam = deelplatform.Naam,
                URLNaam = deelplatform.URLnaam,
                DataOphaalFrequentie = deelplatform.DataOphaalFrequentie
            };
            return PartialView(maakDeelplatformViewModel);
        }
        [HttpPost]
        public ActionResult EditDeelplatform(MaakDeelplatformViewModel deelplatformViewModel)
        {
            if (ModelState.IsValid)
            {
                Deelplatform deelplatform = deelplatformenManager.GetDeelplatform(deelplatformViewModel.Id);
                if (!deelplatform.URLnaam.Equals(deelplatformViewModel.URLNaam, StringComparison.OrdinalIgnoreCase))
                {
                    if (deelplatformenManager.GetDeelplatformByURL(deelplatformViewModel.URLNaam) != null)
                    {
                        ViewBag.Boodschap = "Deze URL is al in gebruik";
                        return PartialView();
                    }

                    //Wanneer de URL van een deelplatform overeenkomt met die van een controller, loopt de routing mis. Dit wordt hier vermeden
                    List<string> controllerNamen = new List<string>() { "Alerts", "Android","IData","Textgain","Account","Admin",
                    "Dashboard", "DashboardStatistieken","GemonitordItem","Overzicht","Profiel","SpecifiekePagina","SuperAdmin" };
                    if (controllerNamen.Any(a => a.Equals(deelplatformViewModel.URLNaam, StringComparison.OrdinalIgnoreCase)))
                    {
                        ViewBag.Boodschap = "Deze URL is niet geldig";
                        return PartialView();
                    }
                    else
                    {
                        deelplatform.URLnaam = deelplatformViewModel.URLNaam;
                    }
                }
                deelplatform.AantalDagenHistoriek = deelplatformViewModel.AantalDagenHistoriek;
                deelplatform.Naam = deelplatformViewModel.Naam;
                deelplatform.URLnaam = deelplatformViewModel.URLNaam;
                deelplatform.DataOphaalFrequentie = deelplatformViewModel.DataOphaalFrequentie;
                if (deelplatform.AfbeeldingPad != null)
                {
                    string oudeAfbeelding = "~/images/Deelplatformen/" + deelplatform.AfbeeldingPad; ;
                    string nieuweAfbeelding = "~/images/Deelplatformen/" + deelplatform.URLnaam + Path.GetExtension(oudeAfbeelding); ;
                    if (System.IO.File.Exists(Server.MapPath(oudeAfbeelding)))
                    {
                        System.IO.File.Move(Server.MapPath(oudeAfbeelding), Server.MapPath(nieuweAfbeelding));
                        deelplatform.AfbeeldingPad = deelplatform.URLnaam + Path.GetExtension(oudeAfbeelding);
                    }
                }
                deelplatformenManager.ChangeDeelplatform(deelplatform);
                ViewBag.Boodschap = "Deelplatform is aangepast";
                return PartialView();
            }
            return PartialView(deelplatformViewModel);
        }

        [HttpGet]
        public ActionResult VerwijderDeelplatform(int id)
        {
            string afbeelding = deelplatformenManager.GetDeelplatform(id).AfbeeldingPad;
            if (afbeelding != null)
            {
                string afbeeldingpad = Server.MapPath("~/images/Deelplatformen/" + afbeelding);
                if (System.IO.File.Exists(afbeeldingpad))
                {
                    System.IO.File.Delete(afbeeldingpad);
                }
            }
            deelplatformenManager.RemoveDeelplatform(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
