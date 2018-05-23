using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Deelplatformen;
using Domain.Gemonitordeitems;

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
