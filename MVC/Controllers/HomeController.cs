using System.Collections.Generic;
using System.Web.Mvc;
using MVC.Models;
using BL.IdentityFramework;
using MVC.Controllers.Api;
using BL;
using Domain.Dashboards;
using Domain.Gemonitordeitems;
using System.Linq;

namespace MVC.Controllers
{
  public partial class HomeController : Controller
  {
    public virtual ActionResult Index()
    {
      return View();
    }

    public virtual ActionResult AddItems()
    {
      DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
      int id = deelplatformenManager.GetDeelplatformByName("Politieke Barometer").DeelplatformId;
      GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
      gemonitordeItemsManager.AddOrganisatie("Open VLD", id, new List<string>() { "Alexander De Croo", "Gwendolyn Rutten", "Maggie De Block" });
      gemonitordeItemsManager.AddOrganisatie("Groen", id, new List<string>() { "Kristof Calvo", "Meyrem Almaci", "Wouter Van Besien" });
      gemonitordeItemsManager.AddOrganisatie("SPA", id, new List<string>() { "Caroline Gennez", "John Crombez", "Bruno Tobback" });
      gemonitordeItemsManager.AddOrganisatie("Vlaams Belang", id, new List<string>() { "Filip Dewinter", "Tom Van Grieken", "Gerolf Annemans" });

      gemonitordeItemsManager.AddThema("Migratie", new List<string>() { "buitenland", "vluchteling", "immigratie", "migratie" }, id);
      gemonitordeItemsManager.AddThema("Fiscaliteit", new List<string>() { "belastingen", "tax", "btw", "sociale zekerheid" }, id);
      gemonitordeItemsManager.AddThema("Milieu", new List<string>() { "kernenergie", "zonnenergie", "steenkool", "luchtvervuiling", "windenergie" }, id);

      List<GemonitordItem> gemonitordeItems = gemonitordeItemsManager.GetGemonitordeItems(1).ToList();
      GrafiekenManager grafiekenManager = new GrafiekenManager();

      List<GemonitordItem> grafiekItems1 = new List<GemonitordItem>()
        {
          gemonitordeItems.ElementAt(0),
          gemonitordeItems.ElementAt(1),
          gemonitordeItems.ElementAt(2)
      };

      Grafiek grafiek1 = new Grafiek()
      {
        DashboardId = 1,
        Periode = 4,
        Titel = "Grafiek van de coole items",
        ToonLegende = true,
        ToonXAs = true,
        ToonYAs = true,
        Type = GrafiekType.line,
        XOnder = false,
        XOorsprongNul = true,
        XTitel = "Items",
        YOorsprongNul = true,
        YTitel = "Waarden",
        Items = grafiekItems1,
        Waarden = new List<GrafiekWaarde>
        {
        GrafiekWaarde.Objectiviteit,
        GrafiekWaarde.Polariteit,
        GrafiekWaarde.Objectiviteit
        }
      };

      grafiekenManager.AddGrafiek(grafiek1);

      List<GemonitordItem> grafiekItems2 = new List<GemonitordItem>()
        {
          gemonitordeItems.ElementAt(3),
          gemonitordeItems.ElementAt(4),
          gemonitordeItems.ElementAt(5)
      };

      Grafiek grafiek2 = new Grafiek()
      {
        DashboardId = 1,
        Periode = 4,
        Titel = "Nog een grafiekje van de coole items",
        ToonLegende = true,
        ToonXAs = true,
        ToonYAs = true,
        Type = GrafiekType.bar,
        XOnder = false,
        XOorsprongNul = true,
        XTitel = "Items",
        YOorsprongNul = true,
        YTitel = "Waarden",
        Items = grafiekItems2,
        Waarden = new List<GrafiekWaarde>
        {
        GrafiekWaarde.Objectiviteit,
        GrafiekWaarde.Polariteit,
        GrafiekWaarde.Objectiviteit
        }
      };

      grafiekenManager.AddGrafiek(grafiek2);

      return RedirectToAction("Index");
    }

    public virtual ActionResult GetData()
    {
      DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
      TextgainController textgainController = new TextgainController();
      textgainController.HaalBerichtenOp(deelplatformenManager.GetDeelplatformByName("Politieke Barometer"));

      return RedirectToAction("Index");
    }
  }
}