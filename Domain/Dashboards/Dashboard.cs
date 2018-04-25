using Domain.Gebruikers;
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
    public Gebruiker Gebruiker { get; set; }
    public int GebruikerId { get; set; }
    public List<Grafiek> Grafieken { get; set; }

    public Dashboard()
    {
      Grafieken = new List<Grafiek>();
    }
  }
}
