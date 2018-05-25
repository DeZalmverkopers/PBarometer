using Domain.Dashboards;
using Domain.Deelplatformen;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Gemonitordeitems
{
    public class GemonitordItem
    {
        public int GemonitordItemId { get; set; }

        public string Naam { get; set; }

        public Trend VermeldingenTrend { get; set; }
        public Trend PolariteitsTrend { get; set; }
        public Trend ObjectiviteitsTrend { get; set; }

        public int TotaalAantalVermeldingen { get; set; }
        public double GemPolariteit { get; set; }
        public double GemObjectiviteit { get; set; }
        public string MeestVoorkomendeURL { get; set; }
        public int AantalBerichtenVanMannen { get; set; }
        public int AantalBerichtenVanVrouwen { get; set; }
        public virtual List<ItemHistoriek> ItemHistorieken { get; set; }

        //Foreign keys
        public List<Alert> Alerts { get; set; }
        public List<DetailItem> DetailItems { get; set; }
        public Deelplatform Deelplatform { get; set; }
        public int? DeelplatformId { get; set; }
        public GemonitordItem()
        {
            DetailItems = new List<DetailItem>();
            ItemHistorieken = new List<ItemHistoriek>();
            Alerts = new List<Alert>();
            VermeldingenTrend = Trend.NEUTRAL;
            PolariteitsTrend = Trend.NEUTRAL;
            ObjectiviteitsTrend = Trend.NEUTRAL;
        }

       

    }
}
