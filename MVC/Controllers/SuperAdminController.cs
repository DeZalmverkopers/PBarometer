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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
  [Authorize(Roles = "SuperAdmin")]
  [RequireHttps]
  public partial class SuperAdminController : Controller
  {
    public DeelplatformenManager deelplatformenManager = new DeelplatformenManager();

    public ApplicationUserManager UserManager
    {
      get
      {
        return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
    }

    private List<ApplicationUser> users
    {
      get
      {
        return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
      }
    }

    // GET: SuperAdmin
    public virtual ActionResult Index()
    {
      return View();
    }

    public virtual ActionResult LaadGemonitordeItemsBeheren()
    {
      return PartialView("~/Views/Shared/AdminSuperadmin/GemonitordeItemsBeheren.cshtml");
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

    public virtual ActionResult LaadMediabronnen()
    {
      return PartialView("~/Views/Shared/Superadmin/SocialeMediabronnenInstellen.cshtml");
    }

    //Laadt de partialview die alle gebruikers uit de databank toont behalve de SuperAdmin zelf.
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

    //Verwijdert een gebruiker uit de databank.
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

      UserManager.Delete(user);
      return RedirectToAction("Index");
    }

    public virtual ActionResult LaadDeelplatform()
    {
      return PartialView("~/Views/Shared/Superadmin/DeelplatformAanmaken.cshtml");
    }

    //Toont in de partialview de gebruikers en of ze al dan niet Adminrechten hebben.
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

    //Geeft of ontneemt een gebruiker Adminrechten.
    [HttpGet]
    public virtual ActionResult SlaAdminOp(string email, bool setAdmin)
    {
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
      }
      else if (isAdmin && !setAdmin)
      {
        UserManager.RemoveFromRole(user.Id, adminRole.Name);
      }
      return RedirectToAction("Index");
    }

    [HttpGet]
    public virtual ActionResult DeelplatformOverzicht()
    {
      return PartialView("OverzichtDeelplatformen", deelplatformenManager.GetDeelplatformen().Select(a => new DeelplatformOverzichtViewModel() { Id = a.DeelplatformId, Naam = a.Naam, URL = a.URLnaam }));
    }

    [HttpGet]
    public ActionResult BeheerDeelplatformen()
    {
      return PartialView();
    }

    [HttpGet]
    public ActionResult MaakDeelplatform()
    {
      return View();
    }

    //Maakt een deelplatform.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult MaakDeelplatform(MaakDeelplatformViewModel maakDeelplatformViewModel)
    {
      var bestand = maakDeelplatformViewModel.Afbeelding;
      if (ModelState.IsValid)
      {
        string afbeeldingPad = null;
        if (bestand != null)
        {
          var bestandsInfo = new FileInfo(maakDeelplatformViewModel.Afbeelding.FileName);
          if (bestandsInfo.Extension.Equals(".png") || bestandsInfo.Extension.Equals(".jpg") || bestandsInfo.Extension.Equals(".jpeg") || bestandsInfo.Extension.Equals(".gif"))
          {
            afbeeldingPad = maakDeelplatformViewModel.URLNaam + bestandsInfo.Extension;

            bestand.SaveAs(HttpContext.Server.MapPath("~/images/Deelplatformen/")
                                                  + afbeeldingPad);

          }
        }
        else
        {
          afbeeldingPad = "default.png";
        }
        deelplatformenManager.AddDeelplatform(new Deelplatform()
        {
          AfbeeldingPad = afbeeldingPad,
          AantalDagenHistoriek = maakDeelplatformViewModel.AantalDagenHistoriek,
          URLnaam = maakDeelplatformViewModel.URLNaam,
          LaatsteSynchronisatie = DateTime.Now.AddYears(-100),
          Naam = maakDeelplatformViewModel.Naam
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
        return RedirectToAction("Index", "Home");
      }
      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult EditDeelplatform(int id)
    {
      DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
      return PartialView(deelplatformenManager.GetDeelplatform(id));
    }
    [HttpPost]
    public ActionResult EditDeelplatform(MaakDeelplatformViewModel deelplatformViewModel)
    {
      if (ModelState.IsValid)
      {
        DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
        Deelplatform deelplatform = deelplatformenManager.GetDeelplatform(deelplatformViewModel.Id);
        deelplatform.AantalDagenHistoriek = deelplatformViewModel.AantalDagenHistoriek;
        deelplatform.Naam = deelplatformViewModel.Naam;
        deelplatform.URLnaam = deelplatformViewModel.URLNaam;
        deelplatformenManager.ChangeDeelplatform(deelplatform);
        ViewBag.Boodschap = "Deelplatform is aangepast";
        return PartialView();
      }
      return PartialView(deelplatformViewModel);
    }

    [HttpGet]
    public ActionResult VerwijderDeelplatform(int id)
    {
      deelplatformenManager.RemoveDeelplatform(id);
      return PartialView("OverzichtDeelplatformen");
    }

    [HttpGet]
    public ActionResult PasDeelplatformAan(int id)
    {
      DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
      deelplatformenManager.GetDeelplatform(id);
      return PartialView("PasDeelplatformAan", deelplatformenManager.GetDeelplatform(id));
    }
    [HttpPost]
    public ActionResult PasDeelplatformAan(MaakDeelplatformViewModel deelplatformViewModel)
    {
      if (ModelState.IsValid)
      {
        DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
        deelplatformenManager.GetDeelplatform(deelplatformViewModel.Id);
        return RedirectToAction("Index", "Home");
      }
      else
      {
        return RedirectToAction("Index");
      }
    }
  }
}