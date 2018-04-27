using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Gemonitordeitems
{
    public class Organisatie : GemonitordItem
    {
        public Organisatie()
        {
            Personen = new List<Persoon>();
        }
        public List<Persoon> Personen { get; set; }

        public override void BerekenEigenschappen()
        {
            List<DetailItem> detailitems = new List<DetailItem>();
            foreach (var persoon in Personen)
            {
                detailitems = detailitems.Concat(persoon.DetailItems).ToList();
            }
            DetailItems = detailitems;

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


        protected override void BerekenGemiddeldePolariteit()
        {
            if (DetailItems.Count > 0 && Personen.Count > 0)
            {
                double polariteit = 0;
                foreach (var persoon in Personen)
                {
                    persoon.BerekenEigenschappen();
                    if (persoon.TotaalAantalVermeldingen > 0)
                    {
                        polariteit += persoon.GemPolariteit * persoon.TotaalAantalVermeldingen;
                    }
                }
                GemPolariteit = polariteit / TotaalAantalVermeldingen;
            }

        }

        protected override void BerekenGemiddeldeObjectiviteit()
        {
            if (DetailItems.Count > 0 && Personen.Count > 0)
            {
                double objectiviteit = 0;
                foreach (var persoon in Personen)
                {
                    persoon.BerekenEigenschappen();
                    if (persoon.TotaalAantalVermeldingen > 0)
                    {
                        objectiviteit += persoon.GemObjectiviteit * persoon.TotaalAantalVermeldingen;
                    }
                }
                GemObjectiviteit = objectiviteit / TotaalAantalVermeldingen;
            }
        }
    }
}
