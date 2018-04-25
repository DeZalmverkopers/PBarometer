using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PB_MVC.Controllers
{
  public partial class BeheerController : Controller
  {
    // GET: Beheer
    public virtual ActionResult Index()
    {
      return View();
    }

    
  }
}