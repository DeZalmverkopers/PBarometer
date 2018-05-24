using Domain.Dashboards;
using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



    public List<Statistiek> GetStatistiekenTest()
    {


      List<Statistiek> statistieken = new List<Statistiek>()
      {
        new Statistiek()
        {
          //StatistiekId    = 1,

          StatistiekIdNietOpslaan = 1,
          GemonitordItemId  = 1,
          //DashboardId = 1,
          //DeelplatformId = 1,
          StatistiekSoort = "getal"
        },
        new Statistiek()
        {
          StatistiekIdNietOpslaan = 2,
          //StatistiekId   = 2,
          GemonitordItemId = 2,
          //DashboardId = 1,
          //DeelplatformId = 1,
          StatistiekSoort = "getal"
        },
        new Statistiek()
        {
           StatistiekIdNietOpslaan = 3,
          //StatistiekId   = 3,
          GemonitordItemId = 3,
          //DashboardId = 1,
          //DeelplatformId = 1,
          StatistiekSoort = "getalTrend"
        },
        new Statistiek()
        {
           StatistiekIdNietOpslaan = 4,
          //StatistiekId   = 4,
          GemonitordItemId = 4,
          //DashboardId = 1,
          //DeelplatformId = 1,
          StatistiekSoort = "getalTrend"

        },

        new Statistiek()
        {
          StatistiekIdNietOpslaan = 5,
          //StatistiekId   = 5,
          GemonitordItemId = 5,
          GemonitordItemId2 = 6,
          StatistiekSoort = "kruising"

        }

      };

      return statistieken;
    }
  }
}

