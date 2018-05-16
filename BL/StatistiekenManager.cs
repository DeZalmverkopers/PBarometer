using Domain.Dashboards;
using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
  public class StatistiekenManager
  {
    public List<Statistiek> GetStatistieken()
    {
      GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();

      List<Statistiek> statistieken = new List<Statistiek>()
      {
        new Statistiek()
        {
          StatistiekId    = 1,
          GemonitordItem  = gemonitordeItemsManager.GetGemonitordeItems(1).ToList()[0],
          SoortStatistiek = "getal"
        },
        new Statistiek()
        {
          StatistiekId   = 2,
          GemonitordItem = gemonitordeItemsManager.GetGemonitordeItems(1).ToList()[1],
          SoortStatistiek = "getal"

        },
        new Statistiek()
        {
          StatistiekId   = 3,
          GemonitordItem = gemonitordeItemsManager.GetGemonitordeItems(1).ToList()[2],
          SoortStatistiek = "getalTrend"

        },
        new Statistiek()
        {
          StatistiekId   = 4,
          GemonitordItem = gemonitordeItemsManager.GetGemonitordeItems(1).ToList()[3],
          SoortStatistiek = "getalTrend"

        },
        new Statistiek()
        {
          StatistiekId   = 5,
          GemonitordItem = gemonitordeItemsManager.GetGemonitordeItems(1).ToList()[4],
          SoortStatistiek = "top5"

        },
        new Statistiek()
        {
          StatistiekId   = 6,
          GemonitordItem = gemonitordeItemsManager.GetGemonitordeItems(1).ToList()[5],
          SoortStatistiek = "top5"

        },
        new Statistiek()
        {
          StatistiekId   = 7,
          GemonitordItem = gemonitordeItemsManager.GetGemonitordeItems(1).ToList()[6],
          SoortStatistiek = "top10"

        },
        new Statistiek()
        {
          StatistiekId   = 8,
          GemonitordItem = gemonitordeItemsManager.GetGemonitordeItems(1).ToList()[7],
          SoortStatistiek = "top10"

        }
    };

      return statistieken;
    }
  }
}

