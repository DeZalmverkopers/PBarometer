using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Seppe

namespace PB_MVC.Models
{
  public class Chart
  {
    public string[] labels { get; set; }
    public List<Datasets> datasets { get; set; }
  }

  public class Datasets
  {
    public string label { get; set; }
    public string[] backgroundColor { get; set; }
    public string[] borderColor { get; set; }
    public string borderWidth { get; set; }
    public int[] data { get; set; }
  }
}