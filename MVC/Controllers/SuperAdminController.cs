using BL.IdentityFramework;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Seppe

namespace MVC.Controllers
{
  [Authorize(Roles = "SuperAdmin")]
  [RequireHttps]
  public partial class SuperAdminController : Controller
  {
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

    public virtual ActionResult LaadGebruikersgegevens()
    {
      return PartialView("~/Views/Shared/Superadmin/GebruikersgegevensNakijken.cshtml");
    }

    public virtual ActionResult LaadDeelplatform()
    {
      return PartialView("~/Views/Shared/Superadmin/DeelplatformAanmaken.cshtml");
    }

    public virtual ActionResult LaadGebruikers()
    {
      List<ApplicationUser> users = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
      List<Models.SuperAdminUserViewModel> userViewModels = new List<Models.SuperAdminUserViewModel>();

      foreach (var user in users)
      {
        if (!user.Email.Equals("superadmin@example.com"))
        {
          userViewModels.Add(new Models.SuperAdminUserViewModel
          {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles
          }
            );
        }
      }
      return PartialView("~/Views/Shared/AdminSuperadmin/LaadGebruikers.cshtml", userViewModels);
    }

    [HttpPost]
    public virtual void LaadGebruikers(Models.SuperAdminUserViewModel model)
    {
      List<ApplicationUser> users = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
      foreach (var user in users)
      {
        //TODO
      }
    }
  }
}