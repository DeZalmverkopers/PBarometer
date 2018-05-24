using Domain.Dashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
 public class StatistiekenRepository
  {

    private EF.DbContext context;

    public StatistiekenRepository()
    {
      context = new EF.DbContext();
    }

    public StatistiekenRepository(UnitOfWork uow)
    {
      context = uow.Context;
    }

    public void CreateStatisitiek(Statistiek statistiek)
    {
      context.Statistieken.Add(statistiek);
      context.SaveChanges();
    }

    public IEnumerable<Statistiek> ReadStatistieken(int dashboardId, int deelplatformId)
    {
      
      return context.Statistieken.AsEnumerable().Where(s => s.DashboardId == dashboardId).Where(s => s.DeelplatformId == deelplatformId);
    }


    public Statistiek ReadStatistiek(int id)
    {     
      return context.Statistieken.AsEnumerable().SingleOrDefault(s => s.StatistiekId == id);
    }

    public void DeleteStatistiek(Statistiek statisitiek)
    {
      context.Statistieken.Remove(statisitiek);
      context.SaveChanges();
    }

  }
}
