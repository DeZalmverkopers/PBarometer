﻿using BL.IdentityFramework;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVC.Models;
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
      List<SuperAdminUserViewModel> userViewModels = new List<SuperAdminUserViewModel>();
      var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

      foreach (ApplicationUser user in users)
      {
        bool ok = true;
        var roles = userManager.GetRoles(user.Id);
        foreach (string role in roles)
        {
          if (role.Equals("SuperAdmin"))
          {
            ok = false;
          }
        }
        if (ok)
        {
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
      }
      return PartialView("~/Views/Shared/AdminSuperadmin/LaadGebruikers.cshtml", userViewModels);
    }

    [HttpGet]
    public virtual ActionResult SlaAdminOp(string email, bool setAdmin)
    {
      ApplicationRoleManager roleManager = new ApplicationRoleManager();
      var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
      List<ApplicationUser> users = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();

      ApplicationUser user = users.Find(u => u.Email.Equals(email));
      var roles = userManager.GetRoles(user.Id);
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
        userManager.AddToRole(user.Id, adminRole.Name);
      }
      else if (isAdmin && !setAdmin)
      {
        userManager.RemoveFromRole(user.Id, adminRole.Name);
      }
      return RedirectToAction("Index");
    }
  }
}