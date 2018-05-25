using Domain.Deelplatformen;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Dashboards
{
    public class Statistiek
    {


    public int StatistiekId { get; set; }

    //Noodzakelijk voor teststatistieken
    [NotMapped]
    public int StatistiekIdNietOpslaan { get; set; }

    public Deelplatform Deelplatform { get; set; }
    public int DeelplatformId { get; set; }
    public int DashboardId { get; set; }
    public Dashboard Dashboard { get; set; }

    public int GemonitordItemId { get; set; }

    //Noodzakelijk voor teststatistieken
    [NotMapped]
    public int GemonitordItemId2 { get; set; }

    //Geen enum omwille van javascript-redenen
    //getal,getalTrend,top5,top10,kruising
    public string StatistiekSoort { get; set; }


  }
}
