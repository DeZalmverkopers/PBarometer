using DAL;
using Domain.Dashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class GrafiekenManager
    {
        private GrafiekenRepository repository;
        private UnitOfWorkManager uowManager;


        public GrafiekenManager()
        {

        }

        public void AddGrafiek(Grafiek grafiek)
        {
            InitNonExistingRepo();
            repository.CreateGrafiek(grafiek);
        }

        public IEnumerable<Grafiek> GetGrafieken(bool dashboard = false, bool items = false)
        {
            InitNonExistingRepo();
            return repository.ReadGrafieken(dashboard, items);
        }

        public IEnumerable<Grafiek> GetGrafieken(string gebruikersId, bool dashboard = false, bool items = false)
        {
            InitNonExistingRepo();
            return repository.ReadGrafieken(dashboard, items).Where(a => a.Gebruiker != null && a.Gebruiker.Id.Equals(gebruikersId));
        }

        public Grafiek GetGrafiek(int id, bool dashboard = false, bool items = false)
        {
            InitNonExistingRepo();
            return repository.ReadGrafiek(id, dashboard, items);
        }

        public void ChangeGrafiek(Grafiek grafiek)
        {
            InitNonExistingRepo();
            repository.UpdateGrafiek(grafiek);
        }

        public void RemoveGrafiek(Grafiek grafiek)
        {
            InitNonExistingRepo();
            repository.DeleteGrafiek(grafiek);
        }
        public void InitNonExistingRepo(bool uow = false)
        {
            if (uow)
            {
                if (uowManager == null)
                {
                    uowManager = new UnitOfWorkManager();
                    repository = new GrafiekenRepository(uowManager.UnitOfWork);
                }
            }
            else
            {
                repository = repository ?? new GrafiekenRepository();
            }
        }
    }
}
