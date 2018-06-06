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
                AantalDagenHistoriek = 7,
                URLnaam = "politiek",
                DataOphaalFrequentie = 2
            };

            //Persoon persoon1 = new Persoon()
            //{
            //    Naam = "Bart De Wever",
            //    Gemeente = "Antwerpen",
            //    Postcode = "2000",
            //    TwitterHandle = "Bart_DeWever",
            //    Facebook = "www.facebook.com/bartdewever/",
            //    Deelplatform = deelplatform
            //};
            //Persoon persoon2 = new Persoon()
            //{
            //    Naam = "Theo Francken",
            //    Gemeente = "Lubbeek",
            //    Postcode = "2310",
            //    TwitterHandle = "FranckenTheo",
            //    Facebook = "//www.facebook.com/franckentheo/",
            //    Website = "https://www.theofrancken.be/",
            //    Deelplatform = deelplatform
            //};
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
                Facebook = "http://www.facebook.com/alexanderdecroo/",
                Website = "https://www.alexanderdecroo.be/",
                Deelplatform = deelplatform
            };
            Persoon persoon6 = new Persoon()
            {
                Naam = "Gwendolyn Rutten",
                Gemeente = "Hasselt",
                Postcode = "3500",
                TwitterHandle = "RuttenGwendolyn",
                Facebook = "http://www.facebook.com/gwendolyn.rutten/",
                Website = "https://www.gwendolynrutten.be/",
                Deelplatform = deelplatform
            };
            Persoon persoon7 = new Persoon()
            {
                Naam = "Wouter Van Besien",
                Gemeente = "Bonheiden",
                Postcode = "2820",
                TwitterHandle = "WouterVanBesien",
                Facebook = "http://www.facebook.com/wouter.vanbesien/",
                Website = "https://www.groen.be/wouter_van_besien",
                Deelplatform = deelplatform
            };
            Persoon persoon8 = new Persoon()
            {
                Naam = "Ben Weyts",
                Gemeente = "Beersel",
                Postcode = "1650",
                TwitterHandle = "BenWeyts",
                Facebook = "http://www.facebook.com/ben.weyts/",
                Website = "https://www.benweyts.be/",
                Deelplatform = deelplatform
            };
            Persoon persoon9 = new Persoon()
            {
                Naam = "Koen Van den Heuvel",
                Gemeente = "Puurs",
                Postcode = "2870",
                TwitterHandle = "KVDHeuvel_VP",
                Facebook = "",
                Website = "http://www.koenvandenheuvel.be/",
                Deelplatform = deelplatform
            };
            Persoon persoon10 = new Persoon()
            {
                Naam = "Bruno Tobback",
                Gemeente = "Leuven",
                Postcode = "3000",
                TwitterHandle = "BTobback",
                Facebook = "",
                Website = "http://www.brunotobback.be/",
                Deelplatform = deelplatform
            };

            Persoon persoon11 = new Persoon()
            {
                Naam = "Johan Van Overtveldt",
                Gemeente = "Mortsel",
                Postcode = "2640",
                TwitterHandle = "jvanovertveldt",
                Facebook = "",
                Website = "http://vanovertveldt.belgium.be/nl/home",
                Deelplatform = deelplatform
            };

            Organisatie nva = new Organisatie()
            {
                Naam = "NVA",
                Personen = new List<Persoon>()
                {
                   persoon8, persoon11
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
                    persoon3, persoon4, persoon7
                },
                Deelplatform = deelplatform
            };
            Organisatie cdv = new Organisatie()
            {
                Naam = "CD&V",
                Personen = new List<Persoon>()
                {
                    persoon9
                },
                Deelplatform = deelplatform
            };
            Organisatie spa = new Organisatie()
            {
                Naam = "sp.a",
                Personen = new List<Persoon>()
                {
                    persoon10
                },
                Deelplatform = deelplatform
            };
            //persoon1.Organisatie = nva;
            //persoon2.Organisatie = nva;
            persoon3.Organisatie = groen;
            persoon4.Organisatie = groen;
            persoon5.Organisatie = vld;
            persoon6.Organisatie = vld;
            persoon7.Organisatie = groen;
            persoon8.Organisatie = nva;
            persoon9.Organisatie = cdv;
            persoon10.Organisatie = spa;
            persoon11.Organisatie = nva;

            Thema thema1 = new Thema() { KernWoorden = new List<string>() { "Migratie", "Immigratie", "Buitenland", "Vreemdeling", "Vluchteling" }, Naam = "Migratie", Deelplatform = deelplatform };
            Thema thema2 = new Thema() { KernWoorden = new List<string>() { "Belastingen", "BTW", "Tax", "Schuld", "Zekerheid" }, Naam = "Fiscaliteit", Deelplatform = deelplatform };
            Deelplatform deelplatform2 = new Deelplatform()
            {
                Naam = "K3 Zoekt K3",
                LaatsteSynchronisatie = DateTime.Now.AddYears(-100),
                AantalDagenHistoriek = 7,
                URLnaam = "k3zoektk3",
                DataOphaalFrequentie = 2,
                AfbeeldingPad = "k3zoektk3.jpg",
            };
            context.Deelplatformen.Add(deelplatform);
            context.Deelplatformen.Add(deelplatform2);
            context.GemonitordeItems.Add(nva);
            context.GemonitordeItems.Add(vld);
            context.GemonitordeItems.Add(spa);
            context.GemonitordeItems.Add(cdv);
            context.GemonitordeItems.Add(groen);
            context.GemonitordeItems.Add(thema1);
            context.GemonitordeItems.Add(thema2);
            context.SaveChanges();


        }
    }
}
