using Domain.Deelplatformen;
using System;
using System.Data.Entity;

namespace DAL.EF
{
  internal class DbInitializer : DropCreateDatabaseAlways<DbContext>
  {
    protected override void Seed(DbContext context)
    {
      Deelplatform deelplatform = new Deelplatform()
      {
        Naam = "Politieke Barometer",
        LaatsteSynchronisatie = DateTime.Now.AddYears(-100),
        AantalDagenHistoriek = 14,
      };

      context.Deelplatformen.Add(deelplatform);
      context.SaveChanges();

      context.SaveChanges();
    }
  }
}
