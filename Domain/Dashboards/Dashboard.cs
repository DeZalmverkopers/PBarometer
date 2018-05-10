﻿using Domain.Deelplatformen;
using Domain.Gebruikers;
using Domain.IdentityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dashboards
{
    public class Dashboard
    {
        //DashboardId: Het Id van het Dashboard.
        public int DashboardId { get; set; }

        //GrafsPerPagina: Het aantal Grafieken per pagina.
        public int GrafsPerPagina { get; set; }

        //Foreign keys
        //Gebruiker: De eigenaar van het Dashboard.
        //Grafieken: De Grafieken die bij het Dashboard horen.
        //public ApplicationUser Gebruiker { get; set; }
        public List<Grafiek> Grafieken { get; set; }
        public int DeelplatformId { get; set; }
        public Deelplatform Deelplatform { get; set; }
        public ApplicationUser Gebruiker { get; set; }
        public Dashboard()
        {
            Grafieken = new List<Grafiek>();
        }
    }
}
