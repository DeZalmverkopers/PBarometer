using Domain.Deelplatformen;
using System;
using System.Data.Entity;

namespace DAL.EF
{
  //De initializer van de databank. De databank wordt alleen gedropt en terug gecreëerd als het model verandert.
  internal class DbInitializer : DropCreateDatabaseIfModelChanges<DbContext>
  {
    protected override void Seed(DbContext context)
    {
      Deelplatform deelplatform = new Deelplatform()
      {
        Naam = "Politieke Barometer",
        LaatsteSynchronisatie = DateTime.Now.AddYears(-100),
        AantalDagenHistoriek = 14,
        URLnaam = "politiek"
      };

      context.Deelplatformen.Add(deelplatform);
      context.SaveChanges();

      context.SaveChanges();
    }
  }
}