using Domain.Deelplatformen;
using Domain.IdentityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dashboards
{
  public class DashboardStatistieken
  {
    //DashboardId: Het Id van het Dashboard.
    public int DashboardStatistiekenId { get; set; }

    //Foreign keys
    //Gebruiker: De eigenaar van het Dashboard.
    //Grafieken: De Grafieken die bij het Dashboard horen.
    //public ApplicationUser Gebruiker { get; set; }
    public List<Statistiek> Statistieken { get; set; }
    public int DeelplatformId { get; set; }
    public Deelplatform Deelplatform { get; set; }
    public ApplicationUser Gebruiker { get; set; }
    public DashboardStatistieken()
    {
      Statistieken = new List<Statistiek>();
    }
  }
}
