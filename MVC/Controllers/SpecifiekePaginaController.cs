using BL;
using Domain.Deelplatformen;
using MVC.Models.Specifieke_Pagina;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC.Controllers
{
  public class SpecifiekePaginaController : Controller
  {
    public DeelplatformenManager manager = new DeelplatformenManager();

    // GET: SpecifiekePagina
    public ActionResult FAQ()
    {
      List<FAQViewModel> models = new List<FAQViewModel>();
      List<FAQItem> FAQItems = manager.GetFAQItems();
      if (FAQItems != null)
      {
        foreach (FAQItem item in FAQItems)
        {
          models.Add(new FAQViewModel() { Vraag = item.Vraag, Antwoord = item.Antwoord });
        }
      }
      return View("~/Views/SpecifiekePagina/FAQ.cshtml", models);
    }
  }
}