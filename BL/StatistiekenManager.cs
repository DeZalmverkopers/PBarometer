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





    public List<Statistiek> GetStatistiekenTest()
    {
      GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();

      //List<GemonitordItem> gemonitordeItems = gemonitordeItemsManager.GetGemonitordeItems(1).ToList();

      List<Statistiek> statistieken = new List<Statistiek>()
      {
        new Statistiek()
        {
          //StatistiekId    = 1,
          GemonitordItemId  = 1,
          DashboardStatistiekId = 1,
          DeelplatformId = 1,
          SoortStatistiek = "getal"
        },
        new Statistiek()
        {
          //StatistiekId   = 2,
          GemonitordItemId = 2,
          DashboardStatistiekId = 1,
          DeelplatformId = 1,
          SoortStatistiek = "getal"

        },
        new Statistiek()
        {
          //StatistiekId   = 3,
          GemonitordItemId = 3,
          DashboardStatistiekId = 1,
          DeelplatformId = 1,
          SoortStatistiek = "getalTrend"

        },
        new Statistiek()
        {
          //StatistiekId   = 4,
          GemonitordItemId = 4,
          DashboardStatistiekId = 1,
          DeelplatformId = 1,
          SoortStatistiek = "getalTrend"

        }
        //,
        //new Statistiek()
        //{
        //  StatistiekId   = 5,
        //  GemonitordItem = gemonitordeItems[4],
        //  SoortStatistiek = "top5"

        //},
        //new Statistiek()
        //{
        //  StatistiekId   = 6,
        //  GemonitordItem = gemonitordeItems[5],
        //  SoortStatistiek = "top5"

        //},
        //new Statistiek()
        //{
        //  StatistiekId   = 7,
        //  GemonitordItem = gemonitordeItems[6],
        //  SoortStatistiek = "top10"

        //},
        //new Statistiek()
        //{
        //  StatistiekId   = 8,
        //  GemonitordItem = gemonitordeItems[7],
        //  SoortStatistiek = "top10"

        //}
    };

      return statistieken;
    }
  }
}

