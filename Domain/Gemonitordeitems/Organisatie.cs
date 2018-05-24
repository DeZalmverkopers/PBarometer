using System.Collections.Generic;
using System.Linq;

namespace Domain.Gemonitordeitems
{
  public class Organisatie : GemonitordItem
  {
    public Organisatie()
    {
      Personen = new List<Persoon>();
    }
    public List<Persoon> Personen { get; set; }

  }
}
