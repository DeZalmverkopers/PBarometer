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

    public List<Grafiek> GetGrafieken(int dashboardId, bool dashboard = false, bool items = false)
    {
      InitNonExistingRepo();

      IEnumerable<Grafiek> alleGrafieken = repository.ReadGrafieken(dashboard, items);
      List<Grafiek> grafieken = new List<Grafiek>();

      foreach(Grafiek grafiek in alleGrafieken)
      {
        if (grafiek.DashboardId == dashboardId)
        {
          grafieken.Add(grafiek);
        }
      }

      return grafieken;
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
