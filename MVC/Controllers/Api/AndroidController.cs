using BL;
using BL.IdentityFramework;
using Domain.Dashboards;
using Domain.Deelplatformen;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using PB_MVC.App_Start;
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
using static PB_MVC.Controllers.AccountController;

namespace PB_MVC.Controllers.Api
{
    public class AndroidController : ApiController
    {   
        [Authorize]
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
                return Ok(deelplatformen);
            }
        }

        [Route("api/Grafieken/")]
        public IHttpActionResult GetGrafieken(string deelplatformnaam, string gebruikersnaam)
        {
            GrafiekenManager grafiekenManager = new GrafiekenManager();
            List<Grafiek> grafieken = grafiekenManager.GetGrafieken().ToList();
            if (grafieken == null || grafieken.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(grafieken);
            }
        }
    }
}