﻿using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
  public partial class OverzichtController : Controller
  {
    // GET: Overzicht
    public virtual ActionResult Index()
    {

      List<String> overzichtsitems = new List<String>()
      {
        "Persoon",
        "Organisatie",
        "Term",
        "Thema"
      };

      ViewBag.Itemslist = overzichtsitems;

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

            return PartialView("Themas", ViewBag);
        }

        public virtual ActionResult LaadTermen()
        {
            return PartialView("Termen");
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
            return PartialView("Personen", ViewBag);
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
            return PartialView("Organisaties", ViewBag);
        }


    }
}