using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using Domain.Dashboards;
using Domain.Gemonitordeitems;

//Seppe

namespace MVC.Controllers
{
	public partial class DashboardController : Controller
	{
		// GET: Dashboard
		public virtual ActionResult Index()
		{
			GrafiekenManager grafiekenManager = new GrafiekenManager();
			List<Grafiek> grafieken = grafiekenManager.GetGrafieken(1, 1, true, true);

			ViewBag.Grafieken = grafieken;

			return View(ViewBag);
		}

		public virtual ActionResult LaadGrafiekAanpassen()
		{
			List<SelectListItem> liX = new List<SelectListItem>
			{
				new SelectListItem { Text = "test1", Value = "test1" },
				new SelectListItem { Text = "test2", Value = "test2" },
				new SelectListItem { Text = "test3", Value = "test3" },
				new SelectListItem { Text = "test4", Value = "test4" },
				new SelectListItem { Text = "test5", Value = "test5" }
			};
			ViewBag.XWaarden = liX;

			List<SelectListItem> liY = new List<SelectListItem>
			{
				new SelectListItem { Text = "dummy1", Value = "test1" },
				new SelectListItem { Text = "dummy2", Value = "test2" },
				new SelectListItem { Text = "dummy3", Value = "test3" },
				new SelectListItem { Text = "dummy4", Value = "test4" },
				new SelectListItem { Text = "dummy5", Value = "test5" }
			};
			ViewBag.YWaarden = liY;

			List<SelectListItem> schaalopties = new List<SelectListItem>
			{
				new SelectListItem { Text = "dagen", Value = "dagen" },
				new SelectListItem { Text = "weken", Value = "weken" },
			};
			ViewBag.Schaalopties = schaalopties;

			return PartialView("~/Views/Shared/Dashboard/GrafiekAanpassen.cshtml", ViewBag);
		}

		public virtual ActionResult LaadLegePartialView()
		{
			return PartialView("~/Views/Shared/LegePartialView.cshtml");
		}
	
	public virtual ActionResult LaadGrafiekToevoegen()
		{
			List<SelectListItem> liGrafiekType = new List<SelectListItem>
			{
				new SelectListItem { Text = "Staafdiagram", Value = "staafdiagram" },
				new SelectListItem { Text = "Taartdiagram", Value = "taartdiagram" },
				new SelectListItem { Text = "Lijndiagram", Value = "lijndiagram" }
			};
			ViewBag.Grafiektypes = liGrafiekType;

			List<SelectListItem> liX = new List<SelectListItem>
			{
				new SelectListItem { Text = "test1", Value = "test1" },
				new SelectListItem { Text = "test2", Value = "test2" },
				new SelectListItem { Text = "test3", Value = "test3" },
				new SelectListItem { Text = "test4", Value = "test4" },
				new SelectListItem { Text = "test5", Value = "test5" }
			};
			ViewBag.XWaarden = liX;

			List<SelectListItem> liY = new List<SelectListItem>
			{
				new SelectListItem { Text = "dummy1", Value = "test1" },
				new SelectListItem { Text = "dummy2", Value = "test2" },
				new SelectListItem { Text = "dummy3", Value = "test3" },
				new SelectListItem { Text = "dummy4", Value = "test4" },
				new SelectListItem { Text = "dummy5", Value = "test5" }
			};
			ViewBag.YWaarden = liY;

			List<SelectListItem> schaalopties = new List<SelectListItem>
			{
				new SelectListItem { Text = "dagen", Value = "dagen" },
				new SelectListItem { Text = "weken", Value = "weken" },
			};
			ViewBag.Schaalopties = schaalopties;

			return PartialView("~/Views/Shared/Dashboard/GrafiekToevoegen.cshtml", ViewBag);
		}

		public virtual ActionResult LaadTop5()
		{
			return PartialView("~/Views/Shared/GetalEnOverzicht/Top5.cshtml");
		}

		public virtual ActionResult LaadStaafdiagram()
		{
			return PartialView("~/Views/Shared/Grafieken/Staafdiagram.cshtml");
		}

		public virtual ActionResult LaadStaafdiagramMulti()
		{
			return PartialView("~/Views/Shared/Grafieken/StaafdiagramMulti.cshtml");
		}

		public virtual ActionResult LaadDonutdiagram()
		{
			return PartialView("~/Views/Shared/Grafieken/Donutdiagram.cshtml");
		}

		public virtual ActionResult LaadAlleGrafieken()
		{
			return PartialView("~/Views/Shared/Grafieken/Grafieken.cshtml");
		}

		public ActionResult VoegGrafiekToeEnUpdateDashboard(string titel, int periode, bool toonLegende, bool toonXAs, bool toonYAs, GrafiekType type,
			string xTitel, string yTitel, bool xOnder, bool xOorsprongNul, bool yOorsprongNul, int dashboardId,
			GemonitordItem item1 = null, GrafiekWaarde waarde1 = GrafiekWaarde.Vermeldingen,
			GemonitordItem item2 = null, GrafiekWaarde waarde2 = GrafiekWaarde.Vermeldingen,
			GemonitordItem item3 = null, GrafiekWaarde waarde3 = GrafiekWaarde.Vermeldingen,
			GemonitordItem item4 = null, GrafiekWaarde waarde4 = GrafiekWaarde.Vermeldingen,
			GemonitordItem item5 = null, GrafiekWaarde waarde5 = GrafiekWaarde.Vermeldingen)
		{
			GrafiekenManager grafiekenManager = new GrafiekenManager();
			List<GemonitordItem> items = new List<GemonitordItem>();
			List<GrafiekWaarde> waarden = new List<GrafiekWaarde>();
			Grafiek grafiek;

			if (item1 != null)
			{
				items.Add(item1);
				waarden.Add(waarde1);

				if (item2 != null)
				{
					items.Add(item2);
					waarden.Add(waarde2);
				}

				if (item3 != null)
				{
					items.Add(item3);
					waarden.Add(waarde3);
				}

				if (item4 != null)
				{
					items.Add(item4);
					waarden.Add(waarde4);
				}

				if (item5 != null)
				{
					items.Add(item5);
					waarden.Add(waarde5);
				}

				grafiek = new Grafiek()
				{
					Titel = titel,
					Periode = periode,
					ToonLegende = toonLegende,
					ToonXAs = toonXAs,
					ToonYAs = toonYAs,
					Type = type,
					XTitel = xTitel,
					YTitel = yTitel,
					Waarden = waarden,
					XOnder = xOnder,
					XOorsprongNul = xOorsprongNul,
					YOorsprongNul = yOorsprongNul,
					DashboardId = dashboardId,
					Items = items
				};

				grafiekenManager.AddGrafiek(grafiek);
			}

			return RedirectToAction("Index");
		}

		public ActionResult UpdateGrafiekEnUpdateDashboard(int id, string titel, int periode, bool toonLegende, bool toonXAs, bool toonYAs, GrafiekType type,
			string xTitel, string yTitel, bool xOnder, bool xOorsprongNul, bool yOorsprongNul, int dashboardId,
			GemonitordItem item1 = null, GrafiekWaarde waarde1 = GrafiekWaarde.Vermeldingen,
			GemonitordItem item2 = null, GrafiekWaarde waarde2 = GrafiekWaarde.Vermeldingen,
			GemonitordItem item3 = null, GrafiekWaarde waarde3 = GrafiekWaarde.Vermeldingen,
			GemonitordItem item4 = null, GrafiekWaarde waarde4 = GrafiekWaarde.Vermeldingen,
			GemonitordItem item5 = null, GrafiekWaarde waarde5 = GrafiekWaarde.Vermeldingen)
		{
			GrafiekenManager grafiekenManager = new GrafiekenManager();
			List<GemonitordItem> items = new List<GemonitordItem>();
			List<GrafiekWaarde> waarden = new List<GrafiekWaarde>();
			Grafiek grafiek;

			if (item1 != null)
			{
				items.Add(item1);
				waarden.Add(waarde1);

				if (item2 != null)
				{
					items.Add(item2);
					waarden.Add(waarde2);
				}

				if (item3 != null)
				{
					items.Add(item3);
					waarden.Add(waarde3);
				}

				if (item4 != null)
				{
					items.Add(item4);
					waarden.Add(waarde4);
				}

				if (item5 != null)
				{
					items.Add(item5);
					waarden.Add(waarde5);
				}

				grafiek = new Grafiek()
				{
					GrafiekId = id,
					Titel = titel,
					Periode = periode,
					ToonLegende = toonLegende,
					ToonXAs = toonXAs,
					ToonYAs = toonYAs,
					Type = type,
					XTitel = xTitel,
					YTitel = yTitel,
					Waarden = waarden,
					XOnder = xOnder,
					XOorsprongNul = xOorsprongNul,
					YOorsprongNul = yOorsprongNul,
					DashboardId = dashboardId,
					Items = items
				};

				grafiekenManager.ChangeGrafiek(grafiek);
			}
			return RedirectToAction("Index");
		}

		public ActionResult VerwijderGrafiekEnUpdateDashboard(int id)
		{
			GrafiekenManager grafiekenManager = new GrafiekenManager();
			Grafiek grafiek = new Grafiek()
			{
				GrafiekId = id
			};

			grafiekenManager.RemoveGrafiek(grafiek);
			return RedirectToAction("Index");
		}
	}
}