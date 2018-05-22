using DAL;
using Domain.Deelplatformen;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BL
{
    public class DeelplatformenManager
    {
        private DeelplatformenRepository repository;
        private UnitOfWorkManager uowManager;

        public DeelplatformenManager()
        {
        }

        public void AddDeelplatform(Deelplatform deelplatform)
        {
            InitNonExistingRepo();
            repository.CreateDeelplatform(deelplatform);
        }

        public IEnumerable<Deelplatform> GetDeelplatformen()
        {
            InitNonExistingRepo();
            return repository.ReadDeelplatformen();
        }

        public Deelplatform GetDeelplatformByName(string naam)
        {
            InitNonExistingRepo();
            return repository.ReadDeelplatformByName(naam);
        }
        public Deelplatform GetDeelplatform(int id, bool relationeleEntiteiten = false)
        {
            InitNonExistingRepo();
            return repository.ReadDeelplatform(id, relationeleEntiteiten);
        }
        public void ChangeDeelplatform(Deelplatform deelplatform)
        {
            InitNonExistingRepo();
            repository.UpdateDeelplatform(deelplatform);
        }

        public void RemoveDeelplatform(int id)
        {
            InitNonExistingRepo();
            Deelplatform deelplatform = GetDeelplatform(id, true);
            //DashboardsManager dashboardsManager = new DashboardsManager();
            GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
            AlertManager alertManager = new AlertManager();
            //dashboardsManager.RemoveDashboards(id);
            //gemonitordeItemsManager.RemoveDetailItems(id);
            alertManager.RemoveAlerts(id);
            gemonitordeItemsManager.RemoveGemonitordeItems(id);

            repository.DeleteDeelplatform(deelplatform);
 
        }



        public Deelplatform GetDeelplatformByURL(string url)
        {
            InitNonExistingRepo();
            return repository.ReadDeelplatformen().FirstOrDefault(a => a.URLnaam.Equals(url, StringComparison.OrdinalIgnoreCase));
        }

        public Settings GetSettings(int deelplatformId)
        {
            InitNonExistingRepo();
            return repository.ReadSettings(deelplatformId);
        }

        public void ChangeOverzichtAdded(bool OverzichtAdded)
        {
            InitNonExistingRepo();
            repository.UpdateOverzichtAdded(OverzichtAdded);
        }

        public void ChangeWeeklyReviewAdded(bool WeeklyReviewAdded)
        {
            InitNonExistingRepo();
            repository.UpdateWeeklyReviewAdded(WeeklyReviewAdded);
        }

        public void ChangeAlertsAdded(bool AlertsAdded)
        {
            InitNonExistingRepo();
            repository.UpdateAlertsAdded(AlertsAdded);
        }

        //public void ChangeSettings(Settings settings)
        //{
        //  repository.UpdateSettings(settings.OverzichtAdded, settings.WeeklyReviewAdded, settings.AlertsAdded);
        //}

        public string GetAchtergrondkleur(int deelplatformId)
        {
            InitNonExistingRepo();
            return repository.ReadAchtergrondkleur(deelplatformId);
        }

        public void ChangeAchtergrondkleur(string kleur)
        {
            InitNonExistingRepo();
            repository.UpdateAchtergrondkleur(kleur);
        }
        public void InitNonExistingRepo(bool uow = false)
        {
            if (uow)
            {
                if (uowManager == null)
                {
                    uowManager = new UnitOfWorkManager();
                    repository = new DeelplatformenRepository(uowManager.UnitOfWork);
                }
            }
            else
            {
                repository = repository ?? new DeelplatformenRepository();
            }
        }
    }
}
