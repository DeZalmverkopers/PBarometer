using Domain.Gemonitordeitems;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Dashboards
{
  public class Grafiek
  {
    public int GrafiekId { get; set; }

    public string Titel { get; set; }

    public bool ToonLegende { get; set; }
    public bool ToonXAs { get; set; }
    public bool ToonYAs { get; set; }

    public bool XOorsprongNul { get; set; }
    public bool YOorsprongNul { get; set; }

    public string XTitel { get; set; }
    public string YTitel { get; set; }
    public bool XOnder { get; set; }

    public List<string> LegendeLijst { get; set; }
    public Dictionary<int, List<dynamic>> Data { get; set; }

    public int Periode { get; set; }
    public GrafiekType Type { get; set; }

    //Foreign keys
    //Dashboard: Het Dashboard waartoe de Grafiek behoort.
    //GemonitordeItems: De GemonitordeItems die de Grafiek gebruikt.
    public Dashboard Dashboard { get; set; }
    public int DashboardId { get; set; }
    public List<GemonitordItem> Items { get; set; }
    
    [NotMapped]
    public List<GrafiekWaarde> Waarden
    {
      get
      {
        return WaardenJSON == null ? null : JsonConvert.DeserializeObject<List<GrafiekWaarde>>(WaardenJSON);
      }
      set
      {
        WaardenJSON = JsonConvert.SerializeObject(value);
      }
    }

    public string WaardenJSON { get; set; }

    public Grafiek()
    {
      LegendeLijst = new List<string>();
      Data = new Dictionary<int, List<dynamic>>();
      Items = new List<GemonitordItem>();
      Waarden = new List<GrafiekWaarde>();
    }
  }
}
