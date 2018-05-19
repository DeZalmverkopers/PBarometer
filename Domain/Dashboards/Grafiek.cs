﻿using Domain.Gemonitordeitems;
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
    //public bool XOnder { get; set; }

    //[NotMapped]
    public List<string> LegendeLijst { get; set; }
    //[NotMapped]
    public List<dynamic> XLabels { get; set; }
    //public Dictionary<int, List<dynamic>> Data { get; set; }

    public List<List<double>> Datawaarden { get; set; }


    //public int Periode { get; set; }
    //[NotMapped]
    //public GrafiekType Type { get; set; }

    public string Type { get; set; }
    //public GrafiekKeuze Keuze { get; set; }

    //Foreign keys
    //Dashboard: Het Dashboard waartoe de Grafiek behoort.
    //GemonitordeItems: De GemonitordeItems die de Grafiek gebruikt.
    //public Dashboard Dashboard { get; set; }
    public int DashboardId { get; set; }
    public int DeelplatformId { get; set; }

    //[NotMapped]
    //public List<GemonitordItem> Items { get; set; }
    //public List<GrafiekItem> GrafiekItems { get; set; }

    public List<List<string>> Randkleur { get; set; }
    public List<List<string>> Achtergrondkleur { get; set; }

    public int XAsMaxrotatie { get; set; }
    public int XAsMinrotatie { get; set; }
    public bool FillDataset { get; set; }
    public bool Lijnlegendeweergave { get; set; }


    //[NotMapped]
    //public List<GrafiekWaarde> Waarden
    //{
    //  get
    //  {
    //    return WaardenJSON == null ? null : JsonConvert.DeserializeObject<List<GrafiekWaarde>>(WaardenJSON);
    //  }
    //  set
    //  {
    //    WaardenJSON = JsonConvert.SerializeObject(value);
    //  }
    //}

    //public string WaardenJSON { get; set; }

    public Grafiek()
    {
      LegendeLijst = new List<string>();
      //Data = new Dictionary<int, List<dynamic>>();
      //Items = new List<GemonitordItem>();
      //GrafiekItems = new List<GrafiekItem>();
      //Waarden = new List<GrafiekWaarde>();
      XLabels = new List<dynamic>();
    }
  }
}
