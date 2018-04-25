using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dashboards
{
  public class Grafiek
  {
    //GrafiekId: Het Id van de Grafiek.
    public int GrafiekId { get; set; }

    //Foreign keys
    //Dashboard: Het Dashboard waartoe de Grafiek behoort.
    //GemonitordeItems: De GemonitordeItems die de Grafiek gebruikt.
    public Dashboard Dashboard { get; set; }
    public int DashboardId { get; set; }
    public List<GemonitordItem> GemonitordeItems { get; set; }

    public Grafiek()
    {
      GemonitordeItems = new List<GemonitordItem>();
    }
  }
}
