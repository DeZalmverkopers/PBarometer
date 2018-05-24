using Domain.Dashboards;
using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;

namespace Domain.Deelplatformen
{
  public class Deelplatform
  {
    public int DeelplatformId { get; set; }
    public string Naam { get; set; }
    public int AantalDagenHistoriek { get; set; }
    public DateTime LaatsteSynchronisatie { get; set; }
    public string AfbeeldingPad { get; set; }
    public string Achtergrondkleur { get; set; }
    public string URLnaam { get; set; }

    public List<GemonitordItem> GemonitordeItems { get; set; }
    public List<Alert> Alerts { get; set; }
    public List<Dashboard> Dashboards { get; set; }
    public List<DetailItem> DetailItems { get; set; }
    public List<Grafiek> Grafieken { get; set; }
    public List<Statistiek> Statistieken { get; set; }
    public List<FAQItem> FAQItems { get; set; }


    public int DataOphaalFrequentie { get; set; }


    public Deelplatform()
    {
      GemonitordeItems = new List<GemonitordItem>();
      Alerts = new List<Alert>();
      DetailItems = new List<DetailItem>();
      Dashboards = new List<Dashboard>();
      FAQItems = new List<FAQItem>();
    }

    //De instelling of het overzicht al dan niet raadpleegbaar is door niet-ingelogde gebruikers.
    public bool OverzichtAdded { get; set; }

    //De instelling of de alerts al dan niet raadpleegbaar is door niet-ingelogde gebruikers.
    public bool AlertsAdded { get; set; }
  }
}
