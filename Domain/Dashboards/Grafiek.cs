using Domain.Gemonitordeitems;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Deelplatformen;

namespace Domain.Dashboards
{

    public class Grafiek
    {

        public int GrafiekId { get; set; }

        [NotMapped]
        public int GrafiekIdNietOpslaan { get; set; }

        public string Titel { get; set; }

        public bool ToonLegende { get; set; }
        public bool ToonXAs { get; set; }
        public bool ToonYAs { get; set; }

        public bool XOorsprongNul { get; set; }
        public bool YOorsprongNul { get; set; }

        public string XTitel { get; set; }
        public string YTitel { get; set; }


        [NotMapped]
        public List<dynamic> LegendeLijst { get; set; }
        [NotMapped]
        public List<dynamic> XLabels { get; set; }
       
        [NotMapped]
        public List<List<double>> Datawaarden { get; set; }
        
        public int Periode { get; set; }
        
        public string Type { get; set; }
        
        //Foreign keys
        //Dashboard: Het Dashboard waartoe de Grafiek behoort.
        //GemonitordeItems: De GemonitordeItems die de Grafiek gebruikt.
        public Dashboard Dashboard { get; set; }
        public int DashboardId { get; set; }
        public int DeelplatformId { get; set; }
        public Deelplatform Deelplatform { get; set; }
       
        [NotMapped]
        public List<GrafiekItem> GrafiekItems
        {
            get
            {
                return GrafiekItemsJSON == null ? null :
                JsonConvert.DeserializeObject<List<GrafiekItem>>(GrafiekItemsJSON);
            }
            set
            {
                GrafiekItemsJSON = JsonConvert.SerializeObject(value);
            }
        }

        public string GrafiekItemsJSON { get; set; }

       
        [NotMapped]
        public List<List<string>> Randkleur
        {
            get
            {
                return RandkleurJSON == null ? null :
                JsonConvert.DeserializeObject<List<List<string>>>(RandkleurJSON);
            }
            set
            {
                RandkleurJSON = JsonConvert.SerializeObject(value);
            }
        }

        public string RandkleurJSON { get; set; }

       
        [NotMapped]
        public List<List<string>> Achtergrondkleur
        {
            get
            {
                return AchtergrondkleurJSON == null ? null :
                JsonConvert.DeserializeObject<List<List<string>>>(AchtergrondkleurJSON);
            }
            set
            {
                AchtergrondkleurJSON = JsonConvert.SerializeObject(value);
            }
        }

        public string AchtergrondkleurJSON { get; set; }



        public GrafiekWaarde GrafiekWaarde { get; set; }


        public bool FillDataset { get; set; }
        public bool Lijnlegendeweergave { get; set; }  

        public Grafiek()
        {
            LegendeLijst = new List<dynamic>();           
            Datawaarden = new List<List<double>>();
            XLabels = new List<dynamic>();
        }
    }
}
