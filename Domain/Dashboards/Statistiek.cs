using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Gemonitordeitems;

namespace Domain.Dashboards
{
 public class Statistiek
  {
    

    public int StatistiekId { get; set; }


    public int DeelplatformId { get; set; }
    public int DashboardStatistiekId { get; set; }


    public int GemonitordItemId { get; set; }

    //getal, getalTrend
    public string SoortStatistiek { get; set; }
  }
}
