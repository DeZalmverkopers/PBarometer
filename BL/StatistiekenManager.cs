using Domain.Dashboards;
using System.Collections.Generic;
using DAL;

namespace BL
{
  public class StatistiekenManager
  {

    private StatistiekenRepository repository;
    private UnitOfWorkManager uowManager;

    public StatistiekenManager()
    {

    }

    public void InitNonExistingRepo(bool uow = false)
    {
      if (uow)
      {
        if (uowManager == null)
        {
          uowManager = new UnitOfWorkManager();
          repository = new StatistiekenRepository(uowManager.UnitOfWork);
        }
      }
      else
      {
        repository = repository ?? new StatistiekenRepository();
      }
    }

    public void AddStatistiek(Statistiek statistiek)
    {
      InitNonExistingRepo();
      repository.CreateStatisitiek(statistiek);
    }

    public IEnumerable<Statistiek> GetStatistieken(int dashboardId, int deelplatformId)
    {
      InitNonExistingRepo();
      return repository.ReadStatistieken(dashboardId, deelplatformId);
    }  

    public Statistiek GetStatistiek(int id)
    {
      InitNonExistingRepo();
      return repository.ReadStatistiek(id);
    }

    public void RemoveStatistiek(Statistiek statistiek)
    {
      InitNonExistingRepo();
      repository.DeleteStatistiek(statistiek);
    }



   
  }
}

