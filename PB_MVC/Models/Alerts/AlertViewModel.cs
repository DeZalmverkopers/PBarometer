﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PB_MVC.Models.Alerts
{
    public class AlertViewModel
    {
        public bool Triggered { get; set; }
        public string Beschrijving { get; set; }
        public string Onderwerp { get; set; }
        public bool Geactiveerd { get; set; }
        public bool Mail { get; set; }
        public bool Mobiel { get; set; }

    }
}