using System.Collections.Generic;

namespace MVC.Models.Android
{
  public class GrafiekDTO
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
    public List<string> XLabels { get; set; }
    public Dictionary<int, List<dynamic>> Data { get; set; }
    public List<string> Waarden { get; set; }
    public int Periode { get; set; }
    public string Type { get; set; }
    public string Keuze { get; set; }
  }
}