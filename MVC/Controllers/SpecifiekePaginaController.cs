using BL;
using BL.IdentityFramework;
using Domain.Deelplatformen;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVC.Models.Specifieke_Pagina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
  public class SpecifiekePaginaController : Controller
  {
    public DeelplatformenManager deelplatformenManager = new DeelplatformenManager();

    public ApplicationUserManager UserManager
    {
      get
      {
        return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
    }

    public Deelplatform HuidigDeelplatform
    {
      get
      {
        return deelplatformenManager.GetDeelplatformByURL(RouteData.Values["deelplatform"].ToString());
      }
    }

    // GET: SpecifiekePagina
    public ActionResult FAQ()
    {
      List<FAQViewModel> models = new List<FAQViewModel>();
      List<FAQItem> FAQItems = deelplatformenManager.GetFAQItems(HuidigDeelplatform.DeelplatformId);
      if (FAQItems != null)
      {
        foreach (FAQItem item in FAQItems)
        {
          models.Add(new FAQViewModel() { Vraag = item.Vraag, Antwoord = item.Antwoord });
        }
      }
      return View("~/Views/SpecifiekePagina/FAQ.cshtml", models);
    }

    public ActionResult NieuweVraag()
    {
      return View();
    }

    [HttpPost]
    public ActionResult NieuweVraag(FAQContactViewModel model)
    {
      List<ApplicationUser> users = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
      List<ApplicationUser> admins = UserManager.GetAdmins(users);

      IdentityMessage message = new IdentityMessage()
      {
        Subject = String.Format("{0} - Nieuwe vraag", HuidigDeelplatform.Naam),
        Body = String.Format("Er is een nieuwe vraag gesteld op het deelplatform {0}: {1}", HuidigDeelplatform.Naam, model.Vraag)
      };

      foreach (ApplicationUser admin in admins)
      {
        message.Destination = admin.Email;
        UserManager.EmailService.Send(message);
      }

      return View("NieuweVraagVerzonden");
    }
  }
}