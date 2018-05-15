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
    public byte[] Afbeelding { get; set; }
    public string Achtergrondkleur { get; set; }

    public List<GemonitordItem> GemonitordeItems { get; set; }

    public Deelplatform()
    {
      GemonitordeItems = new List<GemonitordItem>();
    }
    public bool OverzichtAdded { get; set; }
    public bool WeeklyReviewAdded { get; set; }
    public bool AlertsAdded { get; set; }
  }
}
