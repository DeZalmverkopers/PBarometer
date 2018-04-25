﻿using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Seppe

namespace MVC.Controllers
{
  public partial class GemonitordItemController : Controller
  {
    // GET: GemonitordItem
    public virtual ActionResult Index()
    {
      return View();
    }

    public virtual ActionResult LaadThemas()
    {

      List<Thema> themas = new List<Thema>
      {
        new Thema() {
           Naam = "Energie"
        },
        new Thema() {
           Naam = "Fiscaliteit"
        },
        new Thema(){
          Naam = "Immigratie"
        },
        new Thema(){
          Naam = "Milieu"
        },
        new Thema(){
          Naam = "Mobiliteit"
        },
        new Thema()
        {
          Naam = "Pensioenen"
        },
        new Thema()
        {
          Naam = "Vakbonden"
        }
      };

      ViewBag.Themas = themas;

      return PartialView(MVC.Shared.Views.GemonitordItem.Themas, ViewBag);
    }

    public virtual ActionResult LaadTermen()
    {
      return PartialView(MVC.Shared.Views.GemonitordItem.Termen);
    }

    public virtual ActionResult LaadPersonen()
    {
      List<Persoon> personen = new List<Persoon>
      {
        new Persoon() {
           Naam = "Bart De Wever"
        },
        new Persoon() {
           Naam = "Theo Francken"
        },
        new Persoon() {
           Naam = "Kristof Calvo"
        }
      };
      ViewBag.Personen = personen;
      return PartialView(MVC.Shared.Views.GemonitordItem.Personen, ViewBag);
    }

    public virtual ActionResult LaadOrganisaties()
    {
      List<Organisatie> organisaties = new List<Organisatie>
      {
        new Organisatie() {
           Naam = "CD&V"
        },
         new Organisatie() {
           Naam = "Groen"
        },
          new Organisatie() {
           Naam = "sp.a"
        }
      };
      ViewBag.Organisaties = organisaties;
      return PartialView(MVC.Shared.Views.GemonitordItem.Organisaties, ViewBag);
    }
  }
}