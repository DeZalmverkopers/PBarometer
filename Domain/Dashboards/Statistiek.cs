using Domain.Deelplatformen;

namespace Domain.Dashboards
{
  public class Statistiek
  {
    public int StatistiekId { get; set; }

    public Deelplatform Deelplatform { get; set; }
    public int DeelplatformId { get; set; }
    public int DashboardId { get; set; }
    public Dashboard Dashboard { get; set; }

    public int GemonitordItemId { get; set; }
    public int GemonitordItemId2 { get; set; }

    public string StatistiekSoort { get; set; }


    //public StatistiekSoort StatistiekSoort { get; set; }
  }
}
