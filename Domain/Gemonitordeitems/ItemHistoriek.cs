using System;

namespace Domain.Gemonitordeitems
{
  public class ItemHistoriek
  {
    public DateTime SynchronisatieDatum { get; set; }
    public int AantalVermeldingen { get; set; }
    public double GemPolariteit { get; set; }
    public double GemObjectiviteit { get; set; }
    public int ItemHistoriekId { get; set; }
    public ItemHistoriek()
    {

    }
  }
}