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
                    deelplatformDTOs.Add(new DeelplatformDTO() { Naam = deelplatform.Naam, Id = deelplatform.DeelplatformId });
                }
                return Ok(deelplatformen);
            }
        }

        [Route("api/DeelplatformenAfbeelding")]
        public IHttpActionResult GetDeelplatformAfbeelding(int deelplatformId)
        {
            DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
            Deelplatform deelplatform = deelplatformenManager.GetDeelplatform(deelplatformId);
            if (deelplatform == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(deelplatform.Afbeelding);
            }
        }

        //[Authorize]
        //[Route("api/Grafieken")]
        //public IHttpActionResult GetGrafieken(int deelplatformId)
        //{

        //    GrafiekenManager grafiekenManager = new GrafiekenManager();
        //    List<Grafiek> grafieken = grafiekenManager.GetGrafieken(User.Identity.GetUserId()).ToList();
        //    if (grafieken == null || grafieken.Count() == 0)
        //    {
        //        return StatusCode(HttpStatusCode.NoContent);
        //    }
        //    else
        //    {
        //        return Ok(grafieken);
        //    }
        //}

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