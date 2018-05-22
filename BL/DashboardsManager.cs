using DAL;
using Domain.Dashboards;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
  public class DashboardsManager
  {
    private readonly DashboardsRepository repository;

    public DashboardsManager()
    {
      repository = new DashboardsRepository();
    }

    public void AddDashboard(Dashboard dashboard)
    {
      repository.CreateDashboard(dashboard);
    }

    public IEnumerable<Dashboard> GetDashboards(bool gebruiker = false, bool grafieken = false)
    {
      return repository.ReadDashboards(gebruiker, grafieken);
    }

    public Dashboard GetDashboard(int id, bool gebruiker = false, bool grafieken = false)
    {
      return repository.ReadDashboard(id, gebruiker, grafieken);
    }
    public Dashboard GetDashboardVanGebruikerMetGrafieken(string gebruikersId, int deelplatformId)
    {
      return repository.ReadDashboards(true, true).Where(a => a.Gebruiker != null && a.Gebruiker.Id.Equals(gebruikersId) && a.DeelplatformId == deelplatformId).FirstOrDefault();
    }

    public void ChangeDashboard(Dashboard dashboard)
    {
      repository.UpdateDashboard(dashboard);
    }

    public void RemoveDashboard(Dashboard dashboard)
    {
      repository.DeleteDashboard(dashboard);
    }
  }
}
