//Bart
using DAL;
using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{

    public class GemonitordeItemsManager

    {
        private GemonitordeItemsRepository repository;
        private UnitOfWorkManager uowManager;

        public GemonitordeItemsManager()
        {

        }

        public void AddGemonitordItem(GemonitordItem gemonitordItem)
        {
            InitNonExistingRepo();
            repository.CreateGemonitordItem(gemonitordItem);
        }
        public Persoon GetPersoon(int id, bool organisatie)
        {
            InitNonExistingRepo();
            return repository.ReadPersoon(id, organisatie);
        }

        public IEnumerable<GemonitordItem> GetGemonitordeItems(int deelplatformId)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordeItems().Where(a => a.DeelplatformId == deelplatformId);
        }

        public IEnumerable<GemonitordItem> GetPersonen(int deelplatformId)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordeItems().Where(a => a is Persoon && a.DeelplatformId == deelplatformId);
        }

        public IEnumerable<GemonitordItem> GetGekruisteItems(int deelplatformId)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordeItems().Where(a => a is GekruistItem && a.DeelplatformId == deelplatformId);
        }

        public IEnumerable<Persoon> GetPersonen(int deelplatformId, List<string> personen)
        {
            InitNonExistingRepo();
            List<Persoon> gemonitordeItems = new List<Persoon>();
            var allePersonen = repository.ReadGemonitordeItems().Where(a => a is Persoon && a.DeelplatformId == deelplatformId);
            foreach (var persoon in personen)
            {
                gemonitordeItems.Add(allePersonen.FirstOrDefault(a => a.Naam.Equals(persoon)) as Persoon);
            }
            return gemonitordeItems;
        }

        public void RemoveDetailItems(int deelplatformId)
        {
            InitNonExistingRepo();
            IEnumerable<DetailItem> detailItems = repository.ReadDetailItems().Where(a => a.DeelplatformId == deelplatformId);
            repository.DeleteDetailItems(detailItems);
        }

        public IEnumerable<GemonitordItem> GetThemas(int deelplatformId)
        {
            InitNonExistingRepo();
            //return repository.ReadGemonitordeItems().Where(a => a is Thema && a.DeelplatformId == deelplatformId).Cast<Thema>();
            return repository.ReadGemonitordeItems().Where(a => a is Thema && a.DeelplatformId == deelplatformId);

        }


        public IEnumerable<GemonitordItem> GetOrganisaties(int deelplatformId)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordeItems().Where(a => a is Organisatie && a.DeelplatformId == deelplatformId);
        }

        //public IEnumerable<GemonitordItem> GetThemas(int deelplatformId, bool grafieken = false)
        //{
        //  InitNonExistingRepo();
        //  return repository.ReadGemonitordeItems(grafieken).Where(a => a is Thema && a.DeelplatformId == deelplatformId);
        //}


        public GemonitordItem GetGemonitordItem(int id)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordItem(id);
        }
        public GemonitordItem GetGemonitordItem(int deelplatformId, string naam, bool grafieken = false)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordeItems().FirstOrDefault(a => a.Naam.Equals(naam, StringComparison.OrdinalIgnoreCase) && a.DeelplatformId == deelplatformId);
        }

        public void ChangeGemonitordItem(GemonitordItem gemonitordItem)
        {
            InitNonExistingRepo();
            repository.UpdateGemonitordItem(gemonitordItem);
        }
        public void RemoveGemonitordeItems(int deelplatformId)
        {
            InitNonExistingRepo();
            IEnumerable<GemonitordItem> gemonitordeItems = repository.ReadGemonitordeItems().Where(a => a.DeelplatformId == deelplatformId);
            repository.DeleteGemonitordeItems(gemonitordeItems);
        }
        public void RemoveGemonitordItem(GemonitordItem gemonitordItem)
        {
            InitNonExistingRepo();
            repository.DeleteGemonitordItem(gemonitordItem);
        }
        public void RemovePersoon(int gemonitordItemId) {
            InitNonExistingRepo();
            Persoon persoon = repository.ReadPersoonMetHistoriekenDetailItemsOrganisaties(gemonitordItemId);
            Organisatie organisatie = persoon.Organisatie;
            List<ItemHistoriek> itemHistorieken = persoon.ItemHistorieken;
            List<DetailItem> detailItems = persoon.DetailItems;
            repository.DeleteGemonitordItem(GetGemonitordItem(gemonitordItemId));
            if (organisatie != null)
            {
                BepaalDetailItemsVoorOrganisatie(organisatie);
                BerekenEigenschappen(organisatie);
                repository.UpdateGemonitordItem(organisatie);
            }
            if (itemHistorieken != null)
            {
                repository.DeleteHistorieken(itemHistorieken);
            }
            if (detailItems != null)
            {
                repository.DeleteDetailItems(detailItems);
            }
        }

        public void RemoveGemonitordItem(int id)
        {
            InitNonExistingRepo();
            GemonitordItem gemonitordItem = GetGemonitordItem(id);
            if (gemonitordItem is Persoon)
            {
                RemovePersoon(id);
            }
            else
            {
                repository.DeleteGemonitordItem(gemonitordItem);
            }
            
        }
        public void AddThema(string naam, List<string> kernwoorden, int deelplatformId, bool volgbaar = true)
        {
            InitNonExistingRepo();
            if (GetThemas(deelplatformId).FirstOrDefault(a => a.Naam.Equals(naam, StringComparison.OrdinalIgnoreCase)) == null)
            {
                repository.CreateGemonitordItem(new Thema()
                {
                    Naam = naam,
                    KernWoorden = kernwoorden,
                    TotaalAantalVermeldingen = 0,
                    DeelplatformId = deelplatformId
                });
            }
        }

        public void AddPersoon(string naam, int deelplatformId)
        {
            InitNonExistingRepo();
            if (GetPersonen(deelplatformId).Where(a => a.Naam.Equals(naam, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() == null)
            {
                AddGemonitordItem(new Persoon() { Naam = naam, DeelplatformId = deelplatformId });
            }
        }

        public void AddOrganisatie(string naam, int deelplatformId, List<string> namenPersonen)
        {
            InitNonExistingRepo();
            List<Persoon> toeTeVoegenPersonen = new List<Persoon>();
            if (GetOrganisaties(deelplatformId).FirstOrDefault(a => a.Naam.Equals(naam, StringComparison.OrdinalIgnoreCase)) == null)
            {
                
                List<GemonitordItem> bestaandePersonen = GetPersonen(deelplatformId).ToList();
                foreach (string persoon in namenPersonen)
                {
                    GemonitordItem toeTeVoegenPersoon = bestaandePersonen.FirstOrDefault(a => a.Naam.Equals(persoon, StringComparison.OrdinalIgnoreCase));
                    if (toeTeVoegenPersoon == null)
                    {
                        toeTeVoegenPersoon = new Persoon() { Naam = persoon, TotaalAantalVermeldingen = 0, DeelplatformId = deelplatformId };
                    }
                    toeTeVoegenPersonen.Add(toeTeVoegenPersoon as Persoon);
                }
                Organisatie organisatie = new Organisatie() { Naam = naam, DeelplatformId = deelplatformId, Personen = toeTeVoegenPersonen};
                BepaalDetailItemsVoorOrganisatie(organisatie);
                BerekenEigenschappen(organisatie);
                AddGemonitordItem(organisatie);
            }
        }

        public void EditOrganisatie(int id, int deelplatformId, string naam, List<string> namenPersonen)
        {
            InitNonExistingRepo();
            List<Persoon> toeTeVoegenPersonen = new List<Persoon>();

            Organisatie organisatie = GetGemonitordItem(id) as Organisatie;
            List<GemonitordItem> bestaandePersonen = GetPersonen(deelplatformId).ToList();
            foreach (string persoon in namenPersonen)
            {
                GemonitordItem toeTeVoegenPersoon = bestaandePersonen.FirstOrDefault(a => a.Naam.Equals(persoon.Trim(), StringComparison.OrdinalIgnoreCase));
                if (toeTeVoegenPersoon == null)
                {
                    toeTeVoegenPersoon = new Persoon() { Naam = persoon, TotaalAantalVermeldingen = 0, DeelplatformId = deelplatformId };
                }
                toeTeVoegenPersonen.Add(toeTeVoegenPersoon as Persoon);
            }
            organisatie.Naam = naam;
            organisatie.Personen = toeTeVoegenPersonen;
            BepaalDetailItemsVoorOrganisatie(organisatie);
            BerekenEigenschappen(organisatie);
            ChangeGemonitordItem(organisatie);
        }


        public void AddGekruistItem(GemonitordItem item1, GemonitordItem item2, string naam, int deelplatformId)
        {
            InitNonExistingRepo();
            GemonitordItem gekruistItem = new GekruistItem { Naam = naam, DeelplatformId = deelplatformId, Item1 = item1, Item2 = item2 };
            BerekenEigenschappen(gekruistItem);
            repository.CreateGemonitordItem(gekruistItem);
        }

        public void RefreshItems(DateTime syncDatum, int aantalDagenHistoriek, int deelplatformId)
        {
            InitNonExistingRepo();
            BepaalDetailItemsVoorThemas(deelplatformId);
            VerwijderOudeDetailItems(syncDatum.AddDays(-aantalDagenHistoriek), deelplatformId);
            foreach (Organisatie organisatie in GetOrganisaties(deelplatformId))
            {
                BepaalDetailItemsVoorOrganisatie(organisatie);
            }
            foreach (GekruistItem gekruistitem in GetGekruisteItems(deelplatformId))
            {
                BepaalDetailItemsVoorGekruistItem(gekruistitem);
            }
            foreach (GemonitordItem gemonitordItem in repository.ReadGemonitordeItems().Where(a => a.DeelplatformId == deelplatformId).ToList())
            {
                MaakHistorieken(gemonitordItem, aantalDagenHistoriek, syncDatum);
                BerekenEigenschappen(gemonitordItem);
            }
            AlertManager alertManager = new AlertManager();
            alertManager.GenereerAlerts();
        }


        public Organisatie GetOrganisatie(string naamOrganisatie)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordeItems().Where(a => a.Naam.Equals(naamOrganisatie)).FirstOrDefault() as Organisatie;
        }

        private void VerwijderOudeDetailItems(DateTime limietDatum, int deelplatformId)
        {
            repository.DeleteDetailItems(limietDatum, deelplatformId);
        }


        public void MaakHistorieken(GemonitordItem item, int aantalDagenHistoriek, DateTime syncDatum)
        {
            DateTime startDag;
            if (item.ItemHistorieken == null || item.ItemHistorieken.Count < 1 ||
                item.ItemHistorieken.OrderByDescending(a => a.HistoriekDatum).FirstOrDefault().HistoriekDatum < syncDatum.Date.AddDays(-aantalDagenHistoriek))
            {
                startDag = syncDatum.Date.AddDays(-aantalDagenHistoriek);
            }
            else
            {
                startDag = item.ItemHistorieken.OrderByDescending(a => a.HistoriekDatum).FirstOrDefault().HistoriekDatum;
            }
            if (startDag != syncDatum.Date)
            {
                while (startDag <= syncDatum.Date)
                {
                    DateTime grensDag = startDag.AddDays(1);
                    List<DetailItem> relevanteDetailItems = item.DetailItems.Where(a => a.BerichtDatum > startDag && a.BerichtDatum < grensDag).ToList();
                    if (relevanteDetailItems.Count > 0)
                    {
                        item.ItemHistorieken.Add(new ItemHistoriek()
                        {
                            HistoriekDatum = startDag,
                            AantalVermeldingen = relevanteDetailItems.Count,
                            AantalBerichtenVanMannen = relevanteDetailItems.Where(a => a.ProfielEigenschappen["gender"].Equals("m")).Count(),
                            AantalBerichtenVanVrouwen = relevanteDetailItems.Where(a => a.ProfielEigenschappen["gender"].Equals("f")).Count(),
                            GemObjectiviteit = relevanteDetailItems.Average(a => a.Objectiviteit),
                            GemPolariteit = relevanteDetailItems.Average(a => a.Polariteit),
                        });
                    }
                    else
                    {
                        item.ItemHistorieken.Add(new ItemHistoriek()
                        {
                            HistoriekDatum = startDag,
                            AantalVermeldingen = 0,
                            AantalBerichtenVanMannen = 0,
                            AantalBerichtenVanVrouwen = 0,
                            GemObjectiviteit = 0.5,
                            GemPolariteit = 0
                        });
                    }

                    startDag = grensDag;

                }
            }
            VerwijderHistorieken(item.ItemHistorieken.Where(a => a.HistoriekDatum < syncDatum.Date.AddDays(-aantalDagenHistoriek)).ToList());
           // ChangeGemonitordItem(item);
        }

        private void VerwijderHistorieken(List<ItemHistoriek> itemHistorieken)
        {
            InitNonExistingRepo();
            repository.DeleteHistorieken(itemHistorieken);
        }

        #region BerekenEigenschappen
       

        public virtual void BerekenEigenschappen(GemonitordItem gemonitordItem)
        {
            List<DetailItem> detailItems = gemonitordItem.DetailItems;
            List<ItemHistoriek> itemHistorieken = gemonitordItem.ItemHistorieken;
            if (gemonitordItem.DetailItems != null && gemonitordItem.DetailItems.Count > 0)
            {
                gemonitordItem.TotaalAantalVermeldingen = detailItems.Count;
                BerekenGemiddeldeObjectiviteit(gemonitordItem);
                BerekenGemiddeldePolariteit(gemonitordItem);
                BepaalMeestVoorkomendeURL(gemonitordItem);
                BerekenTotaalAantalVrouwenEnMannen(gemonitordItem);
                if (itemHistorieken != null && itemHistorieken.Count > 0)
                {
                    BerekenPolTrend(gemonitordItem);
                    BerekenObjTrend(gemonitordItem);
                    BerekenVermeldingenTrend(gemonitordItem);
                }
            }
            else
            {
                gemonitordItem.TotaalAantalVermeldingen = 0;
                gemonitordItem.PolariteitsTrend = Trend.NEUTRAL;
                gemonitordItem.ObjectiviteitsTrend = Trend.NEUTRAL;
                gemonitordItem.VermeldingenTrend = Trend.NEUTRAL;
            }
        }

        public void BepaalDetailItemsVoorOrganisatie(Organisatie organisatie)
        {
            List<DetailItem> detailitems = new List<DetailItem>();
            foreach (var persoon in organisatie.Personen)
            {
                detailitems = detailitems.Concat(persoon.DetailItems).ToList();
            }
            organisatie.DetailItems = detailitems;
        }

        public void BepaalDetailItemsVoorGekruistItem(GekruistItem gekruistItem)
        {
            gekruistItem.DetailItems = gekruistItem.Item1.DetailItems.Intersect(gekruistItem.Item2.DetailItems).ToList();
        }


        public void BepaalDetailItemsVoorThemas(int deelplatformId)
        {
            foreach (var thema in GetThemas(deelplatformId).ToList())
            {
                foreach (var detailitem in repository.ReadDetailItems())
                {
                    if (detailitem.Themas.Contains(thema.Naam))
                    {
                        if (thema.DetailItems.FirstOrDefault(a => a.DetailItemId == detailitem.DetailItemId) == null)
                        {
                            thema.DetailItems.Add(detailitem);

                        }
                    }
                }
                ChangeGemonitordItem(thema);
            }
        }

        private void BerekenTotaalAantalVrouwenEnMannen(GemonitordItem gemonitordItem)
        {
            gemonitordItem.AantalBerichtenVanMannen = gemonitordItem.DetailItems.Where(a => a.ProfielEigenschappen["gender"].Equals("m")).Count();
            gemonitordItem.AantalBerichtenVanVrouwen = gemonitordItem.DetailItems.Where(a => a.ProfielEigenschappen["gender"].Equals("f")).Count();
        }

        private void BepaalMeestVoorkomendeURL(GemonitordItem gemonitordItem)
        {
            var url = gemonitordItem.DetailItems.Where(a => a.AndereEigenschappen["urls"].FirstOrDefault() != null).GroupBy(a => a.AndereEigenschappen["urls"].
            FirstOrDefault()).OrderByDescending(b => b.Count()).FirstOrDefault();

            if (url != null)
            {
                gemonitordItem.MeestVoorkomendeURL = url.Key;
            }
            else
            {
                gemonitordItem.MeestVoorkomendeURL = null;
            }

        }

        private void BerekenGemiddeldePolariteit(GemonitordItem gemonitordItem)
        {
            if (gemonitordItem.DetailItems.Count > 0)
            {
                double gemiddelde = 0;
                int teller = 0;
                foreach (var item in gemonitordItem.DetailItems)
                {
                    gemiddelde += item.Polariteit;
                    teller += 1;
                }
                gemonitordItem.GemPolariteit = gemiddelde / teller;
            }
        }
        private void BerekenGemiddeldeObjectiviteit(GemonitordItem gemonitordItem)
        {
            if (gemonitordItem.DetailItems.Count > 0)
            {
                double gemiddelde = 0;
                int teller = 0;
                foreach (var item in gemonitordItem.DetailItems)
                {
                    gemiddelde += item.Objectiviteit;
                    teller += 1;
                }
                gemonitordItem.GemObjectiviteit = gemiddelde / teller;
            }
        }
        private void BerekenPolTrend(GemonitordItem gemonitordItem)
        {
            double gemLaatstePolariteit;
            double gemPolariteitPositief = gemonitordItem.GemPolariteit + 1;
            if (gemonitordItem.DetailItems.Count > 10)
            {
                gemLaatstePolariteit = gemonitordItem.DetailItems.OrderBy(a => a.BerichtDatum).Take(gemonitordItem.DetailItems.Count / 10).Average(a => a.Objectiviteit) + 1;
                if (gemLaatstePolariteit > 0.95 * gemPolariteitPositief && gemLaatstePolariteit < 0.95 * gemPolariteitPositief)
                {
                    gemonitordItem.PolariteitsTrend = Trend.NEUTRAL;
                }
                else if (gemLaatstePolariteit > gemPolariteitPositief * 1.05)
                {
                    gemonitordItem.PolariteitsTrend = Trend.UP;
                }
                else
                {
                    gemonitordItem.PolariteitsTrend = Trend.DOWN;
                }
            }
            else
            {
                gemonitordItem.PolariteitsTrend = Trend.NEUTRAL;
            }
        }

        private void BerekenObjTrend(GemonitordItem gemonitordItem)
        {
            double gemLaatsteObjectiviteit;
            if (gemonitordItem.DetailItems.Count > 10)
            {
                gemLaatsteObjectiviteit = gemonitordItem.DetailItems.OrderBy(a => a.BerichtDatum).Take(gemonitordItem.DetailItems.Count / 10).Average(a => a.Objectiviteit);
                if (gemLaatsteObjectiviteit > 0.95 * gemonitordItem.GemObjectiviteit && gemLaatsteObjectiviteit < 0.95 * gemonitordItem.GemObjectiviteit)
                {
                    gemonitordItem.ObjectiviteitsTrend = Trend.NEUTRAL;
                }
                else if (gemLaatsteObjectiviteit > gemonitordItem.GemObjectiviteit * 1.05)
                {
                    gemonitordItem.ObjectiviteitsTrend = Trend.UP;
                }
                else
                {
                    gemonitordItem.ObjectiviteitsTrend = Trend.DOWN;
                }
            }
            else
            {
                gemonitordItem.ObjectiviteitsTrend = Trend.NEUTRAL;
            }
        }

        private void BerekenVermeldingenTrend(GemonitordItem gemonitordItem)
        {
            int huidigaantalVermeldingen = gemonitordItem.ItemHistorieken.OrderByDescending(a => a.HistoriekDatum).First().AantalVermeldingen;
            double gemiddeldAantalVermeldingen = gemonitordItem.ItemHistorieken.Average(a => a.AantalVermeldingen);
            if (gemiddeldAantalVermeldingen > huidigaantalVermeldingen * 0.9 && gemonitordItem.TotaalAantalVermeldingen < huidigaantalVermeldingen * 1.10)
            {
                gemonitordItem.VermeldingenTrend = Trend.NEUTRAL;
            }
            else if (gemiddeldAantalVermeldingen > huidigaantalVermeldingen * 1.10)
            {
                gemonitordItem.VermeldingenTrend = Trend.DOWN;
            }
            else
            {
                gemonitordItem.VermeldingenTrend = Trend.UP;
            }
        }


        #endregion BerekenEigenschappen

        public void InitNonExistingRepo(bool uow = false)
        {
            if (uow)
            {
                if (uowManager == null)
                {
                    uowManager = new UnitOfWorkManager();
                    repository = new GemonitordeItemsRepository(uowManager.UnitOfWork);
                }
            }
            else
            {
                repository = repository ?? new GemonitordeItemsRepository();
            }
        }
    }
}

