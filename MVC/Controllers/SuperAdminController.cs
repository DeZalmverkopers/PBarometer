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
      return PartialView("GemonitordeItemsBeheren");
    }

    public virtual ActionResult LaadData()
    {
      return PartialView("DataImporterenEnExporteren");
    }

    public virtual ActionResult LaadGebruikersactiviteit()
    {
      return PartialView("GebruikersactiviteitMonitoren");
    }

    public virtual ActionResult LaadLayout()
    {
      return PartialView("LayoutAanpassen");
    }

    public virtual ActionResult LaadMediabronnen()
    {
      return PartialView("SocialeMediabronnenInstellen");
    }

    public virtual ActionResult LaadGebruikersgegevens()
    {
      return PartialView("GebruikersgegevensNakijken");
    }

    public virtual ActionResult LaadDeelplatform()
    {
      return PartialView("DeelplatformAanmaken");
    }

  }
}