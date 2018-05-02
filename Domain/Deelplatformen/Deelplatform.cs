using Domain.Gemonitordeitems;
using Domain.IdentityFramework;
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

    public List<ApplicationUser> Gebruikers { get; set; }
    public List<GemonitordItem> GemonitordeItems { get; set; }

    public Deelplatform()
    {
      Gebruikers = new List<ApplicationUser>();
      GemonitordeItems = new List<GemonitordItem>();
    }
  }
}
