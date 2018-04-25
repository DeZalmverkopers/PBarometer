using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Deelplatformen
{
  public class Deelplatform
  {
    public int DeelplatformId { get; set; }
    public string Naam { get; set; }
    public int AantalDagenHistoriek { get; set; }
    public DateTime LaatsteSynchronisatie { get; set; }
    public byte[] Afbeelding { get; set; }
  }
}
