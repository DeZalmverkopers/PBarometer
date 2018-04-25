using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PB_MVC.Models.Alerts
{
    public class CreateBasicAlertViewModel
    {
        public string Beschrijving { get; set; }
        public string SoortOnderwerp { get; set; }
        public string Onderwerp { get; set; }
        public string Eigenschap { get; set; }
        public Trend Trend { get; set; }
        public bool Mail { get; set; }
        public bool Mobiel { get; set; }
    }
}