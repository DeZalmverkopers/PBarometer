using BL;
using BL.IdentityFramework;
using Domain.Dashboards;
using Domain.Deelplatformen;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MVC.App_Start;
using MVC.Models.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static MVC.Controllers.AccountController;

namespace MVC.Controllers.Api
{
    public class AndroidController : ApiController
    {


        [Route("api/Deelplatformen")]
        public IHttpActionResult GetDeelplatformen()
        {
            DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
            List<Deelplatform> deelplatformen = deelplatformenManager.GetDeelplatformen().ToList();
            if (deelplatformen == null || deelplatformen.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                List<DeelplatformDTO> deelplatformDTOs = new List<DeelplatformDTO>();
                foreach (var deelplatform in deelplatformen)
                {
                    deelplatformDTOs.Add(new DeelplatformDTO() { Naam = deelplatform.Naam, Id = deelplatform.DeelplatformId, Afbeelding = deelplatform.AfbeeldingPad});
                }
                return Ok(deelplatformDTOs);
            }
        }

        //[Route("api/DeelplatformenAfbeelding")]
        //public IHttpActionResult GetDeelplatformAfbeelding(int deelplatformId)
        //{
        //    DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
        //    Deelplatform deelplatform = deelplatformenManager.GetDeelplatform(deelplatformId);
        //    if (deelplatform == null)
        //    {
        //        return StatusCode(HttpStatusCode.NoContent);
        //    }
        //    else
        //    {
        //        return Ok(deelplatform.Afbeelding);
        //    }
        //}

        [Authorize]
        [Route("api/Grafieken")]
       public IHttpActionResult GetGrafieken(int deelplatformId)
        {
            DashboardsManager dashboardsManager = new DashboardsManager();
            List<Grafiek> grafieken = dashboardsManager.GetDashboardVanGebruikerMetGrafieken(User.Identity.GetUserId(), deelplatformId).Grafieken;
            List<GrafiekDTO> grafiekDTOs = new List<GrafiekDTO>();
            if (grafieken == null || grafieken.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                foreach (var grafiek in grafieken)
                {
                    List<string> xlabels = new List<string>();
                    foreach (var item in grafiek.XLabels)
                    {
                        xlabels.Add(item.ToString());
                    }
                    grafiekDTOs.Add(new GrafiekDTO()
                    {
                        GrafiekId = grafiek.GrafiekId,
                        //Data = grafiek.Data,
                        Keuze = grafiek.Keuze.ToString(),
                        LegendeLijst = grafiek.LegendeLijst,
                        Periode = grafiek.Periode,
                        Titel = grafiek.Titel,
                        ToonLegende = grafiek.ToonLegende,
                        ToonXAs = grafiek.ToonXAs,
                        ToonYAs = grafiek.ToonYAs,
                        Type = grafiek.Type.ToString(),
                        XTitel = grafiek.XTitel,
                        YTitel = grafiek.YTitel,
                        YOorsprongNul = grafiek.YOorsprongNul,
                        XLabels = xlabels,
                        XOnder = grafiek.XOnder,
                        XOorsprongNul = grafiek.XOorsprongNul,
                        Waarden = grafiek.Waarden.Select(a => a.ToString()).ToList()
                    });
                }
                return Ok(grafiekDTOs);
            }
        }


        [Authorize]
        [Route("api/Alerts")]
        public IHttpActionResult GetAlerts()
        {
            AlertManager alertManager = new AlertManager();
            List<Alert> alerts = alertManager.GetMobieleAlerts(User.Identity.GetUserId(), true, true).ToList();
            List<AlertDTO> alertDTOs = new List<AlertDTO>();

            foreach (var alert in alerts)
            {
                alertDTOs.Add(new AlertDTO()
                {
                    Beschrijving = alert.Beschrijving,
                    Id = alert.AlertId,
                    Onderwerp = alert.GemonitordItem.Naam,
                    Triggered = alert.Triggered,
                    Geactiveerd = alert.Geactiveerd
                });
            }
            if (alerts == null || alerts.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(alertDTOs);
            }
        }
    }
}