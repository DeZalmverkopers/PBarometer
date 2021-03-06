﻿using Domain.Dashboards;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
  public class GrafiekenRepository
  {
    private EF.DbContext context;

    public GrafiekenRepository()
    {
      context = new EF.DbContext();
    }

    public GrafiekenRepository(UnitOfWork uow)
    {
      context = uow.Context;
    }

    public void CreateGrafiek(Grafiek grafiek)
    {
      context.Grafieken.Add(grafiek);
      context.SaveChanges();
    }

    public IEnumerable<Grafiek> ReadGrafieken(bool dashboard)
    {
      if (!dashboard) return context.Grafieken.AsEnumerable();
      else return context.Grafieken.Include("Dashboard").AsEnumerable();
    }

    public IEnumerable<Grafiek> ReadGrafieken(int dashboardId, bool dashboard)
    {
      if (!dashboard) return context.Grafieken.AsEnumerable().Where(a => a.DashboardId.Equals(dashboardId));
      else return context.Grafieken.Include("Dashboard").AsEnumerable().Where(a => a.DashboardId.Equals(dashboardId));
    }

    public IEnumerable<Grafiek> ReadGrafieken(int dashboardId, int deelplatformId)
    {
      return context.Grafieken.AsEnumerable().Where(g => g.DashboardId == dashboardId).Where(g => g.DeelplatformId == deelplatformId);
    }



    public Grafiek ReadGrafiek(int id, bool dashboard, bool items)
    {
      if (!dashboard && !items) return context.Grafieken.Include("GrafiekItems").AsEnumerable().SingleOrDefault(g => g.GrafiekId == id);
      if (!dashboard && items) return context.Grafieken.Include("GrafiekItems").AsEnumerable().SingleOrDefault(g => g.GrafiekId == id);
      if (dashboard && !items) return context.Grafieken.Include("Dashboard").Include("GrafiekItems").AsEnumerable().SingleOrDefault(g => g.GrafiekId == id);
      else return context.Grafieken.Include("Dashboard").Include("GrafiekItems").AsEnumerable().SingleOrDefault(g => g.GrafiekId == id);
    }

    public void UpdateGrafiek(Grafiek grafiek)
    {
      context.Entry(grafiek).State = EntityState.Modified;
      context.SaveChanges();
    }

    public void DeleteGrafiek(Grafiek grafiek)
    {
      context.Grafieken.Remove(grafiek);
      context.SaveChanges();
    }
  }
}
