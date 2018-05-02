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

        public bool Volgbaar { get; set; }
        public int TotaalAantalVermeldingen { get; set; }
        public double GemPolariteit { get; set; }
        public double GemObjectiviteit { get; set; }
        public virtual List<ItemHistoriek> ItemHistorieken { get; set; }

        //Foreign keys
        public List<Alert> Alerts { get; set; }
        public List<Grafiek> Grafieken { get; set; }
        public List<DetailItem> DetailItems { get; set; }
        public Deelplatform Deelplatform { get; set; }
        public int DeelplatformId { get; set; }
        public GemonitordItem()
        {
            Grafieken = new List<Grafiek>();
            DetailItems = new List<DetailItem>();
            ItemHistorieken = new List<ItemHistoriek>();
            Alerts = new List<Alert>();
        }
        public virtual void BerekenEigenschappen()
        {
            if (DetailItems != null)
            {
                TotaalAantalVermeldingen = DetailItems.Count;
                BerekenGemiddeldeObjectiviteit();
                BerekenGemiddeldePolariteit();
                if (ItemHistorieken != null && ItemHistorieken.Count > 0 && TotaalAantalVermeldingen > 0)
                {
                    BerekenPolTrend();
                    BerekenObjTrend();
                    BerekenVermeldingenTrend();
                }
                else
                {
                    PolariteitsTrend = Trend.NEUTRAL;
                    ObjectiviteitsTrend = Trend.NEUTRAL;
                    VermeldingenTrend = Trend.NEUTRAL;
                }

            }
            else
            {
                TotaalAantalVermeldingen = 0;
            }

        }
        protected virtual void BerekenGemiddeldePolariteit()
        {
            if (DetailItems.Count > 0)
            {
                double gemiddelde = 0;
                int teller = 0;
                foreach (var item in DetailItems)
                {
                    gemiddelde += item.Polariteit;
                    teller += 1;
                }
                GemPolariteit = gemiddelde / teller;
            }
        }
        protected virtual void BerekenGemiddeldeObjectiviteit()
        {
            if (DetailItems.Count > 0)
            {
                double gemiddelde = 0;
                int teller = 0;
                foreach (var item in DetailItems)
                {
                    gemiddelde += item.Objectiviteit;
                    teller += 1;
                }
                GemObjectiviteit = gemiddelde / teller;
            }
        }
        protected virtual void BerekenPolTrend()
        {
            double vorigePolariteit = ItemHistorieken.Last().GemPolariteit;
            if (GemPolariteit > vorigePolariteit * 0.95 && GemPolariteit < vorigePolariteit * 1.05)
            {
                PolariteitsTrend = Trend.NEUTRAL;
            }
            else if (GemPolariteit > vorigePolariteit * 1.05)
            {
                PolariteitsTrend = Trend.UP;
            }
            else
            {
                PolariteitsTrend = Trend.DOWN;
            }
        }

        protected virtual void BerekenObjTrend()
        {
            double vorigeObjectiviteit = ItemHistorieken.Last().GemObjectiviteit;
            if (GemObjectiviteit > vorigeObjectiviteit * 0.95 && GemObjectiviteit < vorigeObjectiviteit * 1.05)
            {
                ObjectiviteitsTrend = Trend.NEUTRAL;
            }
            else if (GemObjectiviteit > vorigeObjectiviteit * 1.05)
            {
                ObjectiviteitsTrend = Trend.UP;
            }
            else
            {
                ObjectiviteitsTrend = Trend.DOWN;
            }
        }

        protected virtual void BerekenVermeldingenTrend()
        {
            int vorigAantalVermeldingen = ItemHistorieken.Last().AantalVermeldingen;
            if (TotaalAantalVermeldingen > vorigAantalVermeldingen * 0.95 && TotaalAantalVermeldingen < vorigAantalVermeldingen * 1.05)
            {
                VermeldingenTrend = Trend.NEUTRAL;
            }
            else if (TotaalAantalVermeldingen > vorigAantalVermeldingen * 1.05)
            {
                VermeldingenTrend = Trend.UP;
            }
            else
            {
                VermeldingenTrend = Trend.DOWN;
            }
        }

    }
}
