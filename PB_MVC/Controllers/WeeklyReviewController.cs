using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
  public partial class WeeklyReviewController : Controller
  {
    // GET: WeeklyReview
    public virtual ActionResult Index()
    {
      return View();
    }
  }
}