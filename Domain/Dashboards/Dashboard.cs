using Domain.Deelplatformen;
using Domain.IdentityFramework;
using System.Collections.Generic;

namespace Domain.Dashboards
{
  public class Dashboard
  {
    //DashboardId: Het Id van het Dashboard.
    public int DashboardId { get; set; }

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
