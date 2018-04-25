using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
  public partial class OverzichtdetailController : Controller
  {
    // GET: Overzichtdetail
    public virtual ActionResult Index()
    {
      return View();
    }
  }
}