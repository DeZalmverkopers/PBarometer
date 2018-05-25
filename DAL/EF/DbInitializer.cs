using Domain.Deelplatformen;
using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
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
                URLnaam = "politiek",
                DataOphaalFrequentie = 1
            };

            Persoon persoon1 = new Persoon()
            {
                Naam = "Bart De Wever",
                Gemeente = "Antwerpen",
                Postcode = "2000",
                TwitterHandle = "Bart_DeWever",
                Facebook = "www.facebook.com/bartdewever/",
                Deelplatform = deelplatform
            };
            Persoon persoon2 = new Persoon()
            {
                Naam = "Theo Francken",
                Gemeente = "Lubbeek",
                Postcode = "2310",
                TwitterHandle = "FranckenTheo",
                Facebook = "//www.facebook.com/franckentheo/",
                Website = "https://www.theofrancken.be/",
                Deelplatform = deelplatform
            };
            Persoon persoon3 = new Persoon()
            {
                Naam = "Kristof Calvo",
                Gemeente = "Mechelen",
                Postcode = "2800",
                TwitterHandle = "kristofcalvo",
                Facebook = "http://www.facebook.com/kristofcalvo/",
                Website = "http://www.kristofcalvo.be/",
                Deelplatform = deelplatform
            }; Persoon persoon4 = new Persoon()
            {
                Naam = "Meyrem Almaci",
                Gemeente = "Antwerpen",
                Postcode = "2000",
                TwitterHandle = "MeyremAlmaci",
                Deelplatform = deelplatform
            };
            Persoon persoon5 = new Persoon()
            {
                Naam = "Alexander De Croo",
                Gemeente = "Vilvoorde",
                Postcode = "1800",
                TwitterHandle = "alexanderdecroo",
                Facebook = "www.facebook.com/alexanderdecroo/",
                Website = "https://www.alexanderdecroo.be/",
                Deelplatform = deelplatform
            };
            Persoon persoon6 = new Persoon()
            {
                Naam = "Gwendolyn Rutten",
                Gemeente = "Hasselt",
                Postcode = "3500",
                TwitterHandle = "RuttenGwendolyn",
                Facebook = "www.facebook.com/gwendolyn.rutten/",
                Website = "https://www.gwendolynrutten.be/",
                Deelplatform = deelplatform
            };

            Organisatie nva = new Organisatie()
            {
                Naam = "NVA",
                Personen = new List<Persoon>()
                {
                    persoon1, persoon2
                },
                Deelplatform = deelplatform
            };
            Organisatie vld = new Organisatie()
            {
                Naam = "Open VLD",
                Personen = new List<Persoon>()
                {
                    persoon5, persoon6
                },
                Deelplatform = deelplatform
            };
            Organisatie groen = new Organisatie()
            {
                Naam = "Groen",
                Personen = new List<Persoon>()
                {
                    persoon3, persoon4
                },
                Deelplatform = deelplatform
            };
            persoon1.Organisatie = nva;
            persoon2.Organisatie = nva;
            persoon3.Organisatie = groen;
            persoon4.Organisatie = groen;
            persoon5.Organisatie = vld;
            persoon6.Organisatie = vld;

            context.Deelplatformen.Add(deelplatform);
            context.GemonitordeItems.Add(nva);
            context.GemonitordeItems.Add(vld);
            context.GemonitordeItems.Add(groen);
            context.SaveChanges();

            
        }
    }
}
