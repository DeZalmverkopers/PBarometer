using BL.IdentityFramework;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity.Owin;
using MVC.Models;
using System;
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
      List<ApplicationUser> users = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
      List<SuperAdminUserViewModel> userViewModels = new List<SuperAdminUserViewModel>();
      foreach (var user in users)
      {
        userViewModels.Add(new SuperAdminUserViewModel
        {
          Email = user.Email,
        }
        );
      }
      return View(userViewModels);
    }

    public virtual ActionResult LaadGemonitordeItemsBeheren()
    {
      return PartialView(MVC.Shared.Views.AdminSuperadmin.GemonitordeItemsBeheren);
    }

    public virtual ActionResult LaadData()
    {
      return PartialView(MVC.Shared.Views.AdminSuperadmin.DataImporterenEnExporteren);
    }

    public virtual ActionResult LaadGebruikersactiviteit()
    {
      return PartialView(MVC.Shared.Views.AdminSuperadmin.GebruikersactiviteitMonitoren );
    }

    public virtual ActionResult LaadLayout()
    {
      return PartialView(MVC.Shared.Views.AdminSuperadmin.LayoutAanpassen);
    }

    public virtual ActionResult LaadMediabronnen()
    {
      return PartialView(MVC.Shared.Views.Superadmin.SocialeMediabronnenInstellen);
    }

    public virtual ActionResult LaadGebruikersgegevens()
    {
      return PartialView(MVC.Shared.Views.Superadmin.GebruikersgegevensNakijken);
    }

    public virtual ActionResult LaadDeelplatform()
    {
      return PartialView(MVC.Shared.Views.Superadmin.DeelplatformAanmaken);
    }

  }
}