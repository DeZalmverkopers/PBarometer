//Bart
using DAL;
using Domain.Bericht;
using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public IEnumerable<GemonitordItem> GetGemonitordeItems(int deelplatformId, bool grafieken = false)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordeItems(grafieken).Where(a => a.DeelplatformId == deelplatformId);
        }

        public IEnumerable<GemonitordItem> GetPersonen(int deelplatformId, bool grafieken = false)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordeItems(grafieken).Where(a => a is Persoon && a.DeelplatformId == deelplatformId);
        }

        public IEnumerable<GemonitordItem> GetOrganisaties(int deelplatformId, bool grafieken = false)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordeItems(grafieken).Where(a => a is Organisatie && a.DeelplatformId == deelplatformId);
        }

        public IEnumerable<GemonitordItem> GetThemas(int deelplatformId, bool grafieken = false)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordeItems(grafieken).Where(a => a is Thema && a.DeelplatformId == deelplatformId);
        }

        public GemonitordItem GetGemonitordItem(int id, bool grafieken = false)
        {
            InitNonExistingRepo();
            return repository.ReadGemonitordItem(id, grafieken);
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

        public void RemoveGemonitordItem(GemonitordItem gemonitordItem)
        {
            InitNonExistingRepo();
            repository.DeleteGemonitordItem(gemonitordItem);
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
                    Volgbaar = true,
                    TotaalAantalVermeldingen = 0,
                    DeelplatformId = deelplatformId
                });
            }
        }

        public void AddPersoon(string naam, int deelplatformId, bool volgbaar = true)
        {
            InitNonExistingRepo();
            if (GetPersonen(deelplatformId).Where(a => a.Naam.Equals(naam, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() == null)
            {
                AddGemonitordItem(new Persoon() { Naam = naam, Volgbaar = volgbaar, DeelplatformId = deelplatformId });
            }
        }

        public void AddOrganisatie(string naam, int deelplatformId, List<string> namenPersonen)
        {
            InitNonExistingRepo();
            List<Persoon> toeTeVoegenPersonen = new List<Persoon>();
            if (GetOrganisaties(deelplatformId).FirstOrDefault(a => a.Naam.Equals(naam, StringComparison.OrdinalIgnoreCase)) == null)
            {
                Organisatie organisatie = new Organisatie() { Naam = naam, Volgbaar = true, TotaalAantalVermeldingen = 0, DeelplatformId = deelplatformId };
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
                AddGemonitordItem(new Organisatie() { Naam = naam, Volgbaar = true, Personen = toeTeVoegenPersonen, DeelplatformId = deelplatformId });
            }
        }

        public void AddGekruistItem(GemonitordItem item1, GemonitordItem item2, string naam, int deelplatformId)
        {
            InitNonExistingRepo();
            GemonitordItem gekruistItem = new GekruistItem { Naam = naam, DeelplatformId = deelplatformId, Item1 = item1, Item2 = item2 };
            gekruistItem.BerekenEigenschappen();
            repository.CreateGemonitordItem(gekruistItem);
        }

        public void RefreshItems(DateTime syncDatum, int aantalDagenHistoriek, int deelplatformId)
        {
            InitNonExistingRepo();
            VoegDetailItemToeAanThemas(deelplatformId);
            VerwijderOudeDetailItems(syncDatum.AddDays(-aantalDagenHistoriek), deelplatformId);
            foreach (var item in repository.ReadGemonitordeItems().Where(a => a.DeelplatformId == deelplatformId).ToList())
            {
                item.BerekenEigenschappen();
                repository.UpdateGemonitordItem(item);
            }
            SaveItemHistoriek(syncDatum);
            AlertManager alertManager = new AlertManager();
            alertManager.GenereerAlerts();
        }

        public void VoegDetailItemToeAanThemas(int deelplatformId)
        {
            foreach (var thema in GetThemas(deelplatformId).ToList())
            {
                foreach (var detailitem in repository.ReadDetailItems())
                {
                    if (detailitem.Themas.Contains(thema.Naam))
                    {
                        thema.DetailItems.Add(detailitem);
                    }
                }
                ChangeGemonitordItem(thema);
            }
        }

        private void VerwijderOudeDetailItems(DateTime limietDatum, int deelplatformId)
        {
            repository.DeleteDetailItems(limietDatum, deelplatformId);
        }


        private void SaveItemHistoriek(DateTime syncDatum)
        {
            DateTime nu = syncDatum;
            foreach (var item in repository.ReadGemonitordeItems().ToList())
            {
                ItemHistoriek historiek = new ItemHistoriek()
                {
                    GemPolariteit = item.GemPolariteit,
                    GemObjectiviteit = item.GemObjectiviteit,
                    AantalVermeldingen = item.TotaalAantalVermeldingen,
                    SynchronisatieDatum = syncDatum
                };
                item.ItemHistorieken.Add(historiek);
                repository.UpdateGemonitordItem(item);
            };
        }

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

