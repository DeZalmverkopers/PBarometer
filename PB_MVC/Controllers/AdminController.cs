using BL.IdentityFramework;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity.Owin;
using MVC.Models;
using System.Collections.Generic;
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
      return PartialView(MVC.Shared.Views.AdminSuperadmin.GemonitordeItemsBeheren);
    }

    public virtual ActionResult LaadData()
    {
      return PartialView(MVC.Shared.Views.AdminSuperadmin.DataImporterenEnExporteren);
    }

    public virtual ActionResult LaadGebruikersactiviteit()
    {
      return PartialView(MVC.Shared.Views.AdminSuperadmin.GebruikersactiviteitMonitoren);
    }

    public virtual ActionResult LaadLayout()
    {
      return PartialView(MVC.Shared.Views.AdminSuperadmin.LayoutAanpassen);
    }
  }
}